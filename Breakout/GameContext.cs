#nullable disable
namespace Breakout;

using Breakout.State;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;

/// <summary>
/// The <see cref="GameContext"/> class is the central class in Breakout, responsible for holding the current game state.
/// </summary>
public class GameContext : DIKUGame, IGameEventProcessor {
    /// <summary>
    /// Since OpenGL has to be initialized first, we need to use a builder method for the context creation.
    /// This is because, if a (e.g.) "new MainMenu()" is passed directly to the ctor of GameContext and
    /// it contains calls to DIKUArcade, an InvalidOperationException will be thrown.
    /// Therefore, GameContext has to be constructed first, and then the initial state can be set. The reason
    /// for not letting the user do this is that GameContext is in an invalid state before SwitchState has been called the first
    /// time, and this method ensures that it has.
    /// </summary>
    /// <typeparam name="T">Type of the initial state.</typeparam>
    /// <param name="windowArgs">Arguments to the base <see cref="DIKUGame"/> class</param>
    /// <returns>A new <see cref="GameContext"/> instance.</returns>

    public static GameContext BuildContext<T>(WindowArgs windowArgs, params object[] ctorArgs) where T : BreakoutState {
        var context = new GameContext(windowArgs);
        var initialState = Activator.CreateInstance(typeof(T), ctorArgs) as T;
        context.SwitchState(initialState);
        return context;
    }

    #region GameContext implementation
    public BreakoutState ActiveState {
        get; private set;
    }
    public bool IsRunning => window.IsRunning();
    private GameContext(WindowArgs windowArgs) : base(windowArgs) {
        window.SetKeyEventHandler((a, k) => ActiveState.HandleKeyEvent(a, k));
        BreakoutBus
            .GetBus()
            .Subscribe(GameEventType.ControlEvent, this);
    }

    /// <summary>
    /// Change the current state of the game. This method will also set <see cref="BreakoutState"/>.Context to reference the current object.
    /// </summary>
    /// <param name="state">The state to switch to.</param>
    public void SwitchState(BreakoutState state) {
        ActiveState = state;
        ActiveState.SetContext(this);
        ActiveState.ResetState();
    }

    public override void Render() {
        ActiveState.RenderState();
    }

    /// <summary>
    /// Update by a single time step. This includes (1) polling for window events, (2) processing events in BreakoutBus and (3) letting the
    /// current state update itself.
    /// </summary>
    public override void Update() {
        // These first two operations are shared among all states, so they are performed here
        // to keep code DRY
        window.PollEvents();
        BreakoutBus
            .GetBus()
            .ProcessEvents();
        ActiveState.UpdateState();
    }
    /// <summary>
    /// Process an event. This method closes the window, and in effect exits the application if a <see cref="GameEventType.ControlEvent"/>
    /// is passed with an "APPLICATION_STOP" message.
    /// </summary>
    /// <param name="gameEvent">The event to process.</param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.ControlEvent &&
            gameEvent.Message == "APPLICATION_STOP") {
            window.CloseWindow();
        }
    }
    #endregion
}
