
namespace BreakoutTests.State;

using Breakout;
using Breakout.State;
using DIKUArcade.GUI;
using DIKUArcade.Input;

[TestFixture]
public class TestMenuState {
    MainMenu menuState;
    GameContext context;

    [SetUp]
    public void SetUp() {
        menuState = new MainMenu();
        context = GameContext.BuildContext<MainMenu>(new WindowArgs { Title = "Breakout v0.1" });
        menuState.SetContext(context);
    }
    [Test]
    public void ArrowKeyUpSwitchesActiveItemPos() {
        // precond
        Assert.That(menuState.GetCurrentState(), Is.EqualTo("New Game"));
        menuState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Up);
        //postcond.
        Assert.That(menuState.GetCurrentState(), Is.EqualTo("Quit"));
    }
    [Test]
    public void ArrowKeyDownSwitchesActiveItemPos() {
        // precond
        Assert.That(menuState.GetCurrentState(), Is.EqualTo("New Game"));

        menuState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        //postcond.
        Assert.That(menuState.GetCurrentState(), Is.EqualTo("Quit"));
    }
    [Test]
    public void TestRedirectToGameRunning() {
        // precond
        Assert.That(context.ActiveState, Is.TypeOf<MainMenu>());
        menuState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        //postcond
        Assert.That(context.ActiveState, Is.TypeOf<GameRunning>());
    }

    [Test]
    public void TestKeyReleaseIsNoop() {
        Assert.That(menuState.GetCurrentState(), Is.EqualTo("New Game"));
        menuState.HandleKeyEvent(KeyboardAction.KeyRelease, KeyboardKey.K);
        Assert.That(menuState.GetCurrentState(), Is.EqualTo("New Game"));
    }
}