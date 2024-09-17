
namespace BreakoutTests.State;

using Breakout;
using Breakout.State;
using DIKUArcade.GUI;
using DIKUArcade.Input;

[TestFixture]
public class MainMenuTest {
    MainMenu mainMenu;
    GameContext context;

    [SetUp]
    public void SetUp() {
        mainMenu = new MainMenu();
        context = GameContext.BuildContext<MainMenu>(new WindowArgs { Title = "Breakout v0.1" });
        mainMenu.SetContext(context);
    }
    [Test]
    public void TestChangeStateToGameRunning() {
        // precond
        Assert.That(mainMenu.GetCurrentState(), Is.EqualTo("New Game"));

        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        //postcond
        Assert.That(context.ActiveState, Is.TypeOf<GameRunning>());
    }
    [Test]
    public void TestQuitGame() {
        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Down);
        // precond
        Assert.That(context.IsRunning, Is.True);
        Assert.That(mainMenu.GetCurrentState(), Is.EqualTo("Quit"));

        mainMenu.HandleKeyEvent(KeyboardAction.KeyPress, KeyboardKey.Enter);
        BreakoutBus.GetBus().ProcessEventsSequentially();
        // postcond.
        Assert.That(context.IsRunning, Is.False);
    }

}