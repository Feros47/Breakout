namespace BreakoutTests.State;

using System.Linq;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;

[TestFixture]
public class GameRunningTests {

    private GameContext context;
    private GameRunning gameRunning;
    private HealthManager healthManager;
    private PointManager pointManager;

    [SetUp]
    public void SetUp() {
        healthManager = new HealthManager();
        pointManager = new PointManager();
        gameRunning = new GameRunning(1, healthManager, pointManager);
        context = GameContext.BuildContext<GameRunning>(new WindowArgs { Title = "Breakout v0.1" }, 1, healthManager, pointManager);
        gameRunning.SetContext(context);
        BreakoutBus.ClearEvents();
    }

    [Test]
    public void TestGameRunning() {
        Assert.That(gameRunning.GetEntities().Count, Is.EqualTo(80));
    }

    [Test]
    public void NoneCollides() {
        // 3 life entities are added
        gameRunning.UpdateState();
        gameRunning.UpdateState();
        gameRunning.UpdateState();
        // Which should put the count to 83
        Assert.That(gameRunning.GetEntities().Count, Is.EqualTo(83));
    }

    [Test]
    public void Collisions() {
        gameRunning.UpdateState();
        int prevCount = gameRunning.GetEntities().OfType<BaseBlock>().Count();
        var ball = gameRunning.GetEntities().OfType<Ball>().FirstOrDefault()!;
        ball.Shape.AsDynamicShape().ChangeDirection(new Vec2F(0f, 0.1f));
        ball.Shape.SetPosition(new Vec2F(0.5f, 0.7f));
        for (int i = 0; i < 10; i++) {
            gameRunning.UpdateState();
        }
        gameRunning.UpdateState();
        Assert.That(gameRunning.GetEntities().OfType<BaseBlock>().Count, Is.LessThan(prevCount));
    }

    [Test]
    public void GamePaused() {
        //precond
        Assert.That(context.ActiveState, Is.TypeOf<GameRunning>());
        gameRunning.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Escape);
        Assert.That(context.ActiveState, Is.TypeOf<GamePaused>());
    }

    [Test]
    public void LoseGame() {
        Assert.That(context.ActiveState, Is.TypeOf<GameRunning>());
        gameRunning.UpdateState();
        while (healthManager.Health > 0) {
            healthManager.DecreaseHealth();
        }
        gameRunning.UpdateState();
        Assert.That(context.ActiveState, Is.TypeOf<LoseState>());
    }

    [Test]
    public void WinGame() {
        Assert.That(context.ActiveState, Is.TypeOf<GameRunning>());
        context = GameContext.BuildContext<GameRunning>(new WindowArgs { Title = "Breakout v0.1" }, 4, new HealthManager(), new PointManager());
        var gameRunning = (context.ActiveState as GameRunning)!;
        var blocks = gameRunning.GetEntities().OfType<BaseBlock>();
        foreach (var block in blocks) {
            block.DeleteBlock();
        }
        context.ActiveState.UpdateState();
        Assert.That(context.ActiveState, Is.TypeOf<WinState>());
    }

}
