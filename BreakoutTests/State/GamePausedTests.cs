#nullable disable
namespace BreakoutTests.State;
using Breakout;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using NUnit.Framework;

[TestFixture]
public class GamePausedTests {

    private WindowArgs windowArgs;
    private GamePaused pauseState;
    private GameContext context;

    [SetUp]
    public void Setup() {
        windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        pauseState = new GamePaused(new GameRunning(1, new HealthManager(), new PointManager()));
        context = GameContext.BuildContext<GamePaused>(new WindowArgs { Title = "Breakout v0.1" }, new GameRunning(1, new HealthManager(), new PointManager()));
    }

    [Test]
    public void TestRedirectToMainMenu() {
        pauseState.SetContext(context);
        Assert.That(context.ActiveState, Is.TypeOf<GamePaused>());
        pauseState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        pauseState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(context.ActiveState, Is.TypeOf<MainMenu>());
    }

    [Test]
    public void TestContinueGame() {
        pauseState.SetContext(context);
        Assert.That(context.ActiveState, Is.TypeOf<GamePaused>());
        pauseState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        Assert.That(context.ActiveState, Is.TypeOf<GameRunning>());
    }

}
