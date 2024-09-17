#nullable disable
namespace Breakout.State;

using DIKUArcade.Input;
using DIKUArcade.State;

/// <summary> 
/// The abstract class for the BreakoutStates
/// </summary>
public abstract class BreakoutState : IGameState {
    /// <summary>
    /// Set the context for a state
    /// </summary>
    /// <param name="context"></param>
    public void SetContext(GameContext context) {
        Context = context;
    }
    public GameContext Context {
        get;
        private set;
    }
    public abstract void ResetState();
    public abstract void UpdateState();
    public abstract void RenderState();
    public abstract void HandleKeyEvent(KeyboardAction action, KeyboardKey key);
}
