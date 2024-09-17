namespace BreakoutTests.Entities.Hazards;

using System.Linq;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Math;

public class SlimJimStrategyTests : BreakoutTestBase {

    private SlimJimStrategy strategy;
    private GameRunning game;
    private HealthManager healthManager;

    [SetUp]
    public void SetUp() {
        strategy = new();
        healthManager = new HealthManager();
        game = new GameRunning(1, healthManager, new PointManager());
    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<SlimJimStrategy>());
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
        Assert.That(widthAfter, Is.LessThan(widthBefore));
    }
}