namespace BreakoutTests.Entities.Blocks.Decorators;
using System.Linq;
using Breakout;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.CreationStrategies;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Managers;
using Breakout.State;
using BreakoutTests.Stubs;
using DIKUArcade.Entities;

internal class PowerUpBlockDecoratorTests : BreakoutTestBase {

    private PowerUpBlockDecorator decorator;
    private BaseBlock decorated;
    private GameRunning game;

    [SetUp]
    public void Setup() {
        decorated = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new ImageStub());
        decorator = new PowerUpBlockDecorator(decorated, CreateStrategy());
        game = new GameRunning(1, new HealthManager(), new PointManager());
    }

    [Test]
    public void TestIsHazard() {
        Assert.True(decorator.IsBlockType<Block>());
        Assert.True(decorator.IsBlockType<PowerUpBlockDecorator>());
    }

    private IDropItemCreationStrategy CreateStrategy() {
        return new LaserStrategy();
    }

    [Test]
    public void TestHandleCollision() {
        var blockPosX = decorated.Shape.Position.X;
        var blockPosY = decorated.Shape.Position.Y;
        decorator.HandleCollision();
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var item = game.GetEntities().OfType<DropItem>().FirstOrDefault();
        Assert.That(item!.Shape.Position.X, Is.EqualTo(blockPosX).Within(1e-05));
        Assert.That(item!.Shape.Position.Y, Is.EqualTo(blockPosY).Within(1e-05));
    }

    [Test]
    public void TestRenderStub() {
        decorator = new PowerUpBlockDecorator(new BaseBlockStub(), CreateStrategy());
        Assert.Throws<StubException>(() => decorator.Render(), "RENDER_BASEBLOCK");
    }

    [Test]
    public void TestRender() {
        decorator = new PowerUpBlockDecorator(decorated, new StrategyStub());
        Assert.Throws<StubException>(() => decorator.Render(), "STRATEGY_ICON");
    }
}