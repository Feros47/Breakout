namespace BreakoutTests.Entities.Hazards;

using Breakout;
using Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Math;

public class DecreaseHealthItemTests : BreakoutTestBase {

    private DecreaseHealthStrategy strategy;
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
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<DecreaseHealthStrategy>());
    }
    [Test]
    public void DecreaseHealth() {
        // initialize three health in HealthManager()
        game.UpdateState();
        game.UpdateState();
        game.UpdateState();
        //precond.
        var before = healthManager.Health;
        strategy.CreateExpendable(new Vec2F(0f, 0f));
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var after = healthManager.Health;
        // postcond.
        Assert.That(before, Is.GreaterThan(after));

    }
}