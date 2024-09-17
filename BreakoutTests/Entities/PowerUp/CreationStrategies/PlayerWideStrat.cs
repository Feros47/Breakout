namespace BreakoutTests.Entities.PowerUp.CreationStrategies;

using System.Linq;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Math;


[TestFixture]
public class PlayerWideStrat : BreakoutTestBase {
    private PlayerWidthStrategy strategy;
    private GameRunning game;
    [SetUp]
    public void SetUp() {
        strategy = new PlayerWidthStrategy();
        game = new GameRunning(1, new HealthManager(), new PointManager());
    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<PlayerWidthStrategy>());
    }


    [Test]
    public void PlayerWidthChanged() {
        var widthBefore = game.GetEntities()
            .OfType<Player>()
            .FirstOrDefault()!
            .GetShape().Extent.X;
        strategy.CreateExpendable(new Vec2F(0f, 0f));
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var widthAfter = game.GetEntities()
            .OfType<Player>()
            .FirstOrDefault()!
            .GetShape().Extent.X;
        Assert.That(widthAfter, Is.GreaterThan(widthBefore));
    }

}