namespace Breakout.State;

using DIKUArcade.Input;
using DIKUArcade.Timers;

/// <summary>
/// The class of which is responible for rendering the paused state and changing to either the game running state or the main menu state
/// </summary>
public class GamePaused : MenuState {
    private readonly GameRunning gameRunning;
    public GamePaused(GameRunning gameRunning) : base(new[] { "Continue", "Menu" }) {
        StaticTimer.PauseTimer();
        this.gameRunning = gameRunning;
    }
    protected override void HandleKeyPressEvent(KeyboardKey key, string activeLabel) {
        if (key == KeyboardKey.Enter) {
            StaticTimer.ResumeTimer();
            if (activeLabel == "Continue") {
                Context.SwitchState(gameRunning);
            } else {
                Context.SwitchState(new MainMenu());
            }
        }
    }
}
