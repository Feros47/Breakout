namespace Breakout.State;

using DIKUArcade.Entities;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Utility;

/// <summary> 
/// An abstract class used for creating the menucontainer and creating the menu background
public abstract class MenuState : BreakoutState {
    private static readonly Vec3I baseColor = new Vec3I(255, 255, 255);
    private static readonly Vec3I activeColor = new Vec3I(255, 255, 155);

    private readonly Entity backgroundImage;
    private readonly MenuContainer menu;
    private readonly MenuContainer.CircularListEnumerator activeItem;
    public MenuState(IEnumerable<string> labels) {
        backgroundImage = new Entity(
            new StationaryShape(0.0f, 0.0f, 1.0f, 1.0f),
            DIKUArcadeExtensions.LoadImageFromAssets("BreakoutTitleScreen.png"));
        menu = new MenuContainer(labels, new Vec2F(0.25f, 0.15f), new Vec2F(0.5f, 0.35f));
        activeItem = menu.GetConcreteEnumerator();
    }

    /// <summary>
    /// Get the state that is currently chosen
    /// </summary>
    /// <returns></returns>
    public string GetCurrentState() {
        return activeItem.Current;
    }
    /// <summary>
    /// Reset the active item to be the first element
    /// </summary>
    public override void ResetState() {
        activeItem.Reset();
    }
    /// <summary>
    /// Update the menu state, setting the correct colors for each item
    /// </summary>
    public override void UpdateState() {
        menu.ForEach(mi => mi.SetColor(baseColor));
        activeItem.SetColor(activeColor);
    }
    /// <summary>
    /// Render the menu
    /// </summary>
    public override void RenderState() {
        backgroundImage.RenderEntity();
        menu.Render();
        RenderInternal();
    }
    public override void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyRelease) {
            return;
        }

        switch (key) {
            case KeyboardKey.Up:
                activeItem.MoveUp();
                break;
            case KeyboardKey.Down:
                activeItem.MoveDown();
                break;
            default: // defer to child
                HandleKeyPressEvent(key, activeItem.Current);
                break;
        }
    }

    protected virtual void RenderInternal() {
        // Intentional no-op: If child doesn't have anything to render, don't force it to implement the function.
    }
    protected abstract void HandleKeyPressEvent(KeyboardKey key, string activeLabel);
}
