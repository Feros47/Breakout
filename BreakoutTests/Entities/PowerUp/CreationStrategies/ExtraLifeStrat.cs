namespace BreakoutTests.Entities.PowerUp.CreationStrategies;

using Breakout;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Math;


[TestFixture]
public class ExtraLifeStrat : BreakoutTestBase {
    private PlayerHealthStrategy strategy;
    private GameRunning game;
    private HealthManager healthManager;
    [SetUp]
    public void SetUp() {
        healthManager = new HealthManager();
        strategy = new PlayerHealthStrategy();
        game = new GameRunning(1, healthManager, new PointManager());
    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<PlayerHealthStrategy>());
    }

    [Test]
    public void AddHealth() {
        //precond.
        var before = healthManager.Health;
        strategy.CreateExpendable(new Vec2F(0f, 0f));
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var after = healthManager.Health;
        // postcond.
        Assert.That(before, Is.LessThan(after));

    }

}