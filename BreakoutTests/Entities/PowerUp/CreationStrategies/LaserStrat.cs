namespace BreakoutTests.Entities.PowerUp.CreationStrategies;

using System.Linq;
using Breakout;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Entities.DropItem.PowerUps;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Math;

[TestFixture]
public class LaserStrat : BreakoutTestBase {
    private LaserStrategy strategy;
    private GameRunning game;
    [SetUp]
    public void SetUp() {
        strategy = new();
        game = new GameRunning(1, new HealthManager(), new PointManager());
    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<LaserStrategy>());
    }

    [Test]
    public void LaserAdded() {
        var before = game.GetEntities()
            .OfType<PowerUpExpendLaser>()
            .Count();
        strategy.CreateExpendable(new Vec2F(0f, 0f));
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var after = game.GetEntities()
            .OfType<PowerUpExpendLaser>()
            .Count();
        Assert.That(after, Is.GreaterThan(before));
    }
}