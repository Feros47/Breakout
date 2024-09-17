namespace BreakoutTests.Entities.PowerUp.CreationStrategies;

using System.Linq;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Math;


[TestFixture]
public class BallSplitStrat : BreakoutTestBase {
    private BallSplitStrategy strategy;
    private GameRunning game;
    [SetUp]
    public void SetUp() {
        strategy = new BallSplitStrategy();
        game = new GameRunning(1, new HealthManager(), new PointManager());

    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<BallSplitStrategy>());
    }

    [Test]
    public void TwoBallsAdded() {
        var ballsBefore = game.GetEntities()
            .OfType<Ball>()
            .Count();
        strategy.CreateExpendable(new Vec2F(0f, 0f));
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var ballsAfter = game.GetEntities()
            .OfType<Ball>()
            .Count();
        Assert.That(ballsAfter, Is.EqualTo(ballsBefore + 2));
    }
}