
namespace BreakoutTests.State;

using Breakout;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.GUI;
using DIKUArcade.Input;

[TestFixture]
public class WinStateTest {
    WinState winState;
    GameContext context;

    [SetUp]
    public void SetUp() {
        winState = new WinState(new PointManager());
        context = GameContext.BuildContext<WinState>(new WindowArgs { Title = "Breakout v0.1" }, new PointManager());
    }

    [Test]
    public void TestRedirectToMainMenu() {
        winState.SetContext(context);
        // precond
        Assert.That(context.ActiveState, Is.TypeOf<WinState>());
        winState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        //postcond
        Assert.That(context.ActiveState, Is.TypeOf<MainMenu>());
    }

}