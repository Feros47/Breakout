namespace BreakoutTests.State;

using Breakout;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.GUI;
using DIKUArcade.Input;

[TestFixture]
public class LoseStateTest {
    LoseState loseState;
    GameContext context;

    [SetUp]
    public void SetUp() {
        loseState = new LoseState(new PointManager());
        context = GameContext.BuildContext<LoseState>(new WindowArgs { Title = "Breakout v0.1" }, new PointManager());
    }
    [Test]
    public void TestRedirectToMainMenu() {
        loseState.SetContext(context);
        // precond
        Assert.That(context.ActiveState, Is.TypeOf<LoseState>());
        loseState.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        //postcond
        Assert.That(context.ActiveState, Is.TypeOf<MainMenu>());
    }

}