namespace Breakout.State;

using DIKUArcade.Events;
using DIKUArcade.Input;
using Managers;

/// <summary>
/// State representing the main menu of a game
/// </summary>
public class MainMenu : MenuState {

    private HealthManager healthManagerInstance;
    private PointManager pointManagerInstance;

    public MainMenu() : base(new[] { "New Game", "Quit" }) {
        healthManagerInstance = new HealthManager();
        pointManagerInstance = new PointManager();
    }

    protected override void HandleKeyPressEvent(KeyboardKey key, string activeLabel) {
        switch (key) {
            case KeyboardKey.Enter:
                if (activeLabel == "New Game") {
                    Context.SwitchState(new GameRunning(1, healthManagerInstance, pointManagerInstance));
                } else {
                    // goto is justified here since it is just a "fallthrough" statement
                    goto case KeyboardKey.Escape;
                }
                break;
            case KeyboardKey.Escape:
                BreakoutBus
                    .GetBus()
                    .RegisterEvent(new GameEvent {
                        EventType = GameEventType.ControlEvent,
                        Message = "APPLICATION_STOP",
                        To = Context
                    });
                break;
        }
    }
}
