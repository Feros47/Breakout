namespace Breakout.State;

using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Managers;

/// <summary>
/// The state to be shown after a game is won
/// </summary>
public class WinState : MenuState {
    private readonly Text winBanner;
    private readonly Text pointsBanner;
    public WinState(PointManager pointmanager) : base(new[] { "Menu" }) {
        winBanner = new Text(
            "YOU WIN",
            new Vec2F(0.2f, 0.0f),
            new Vec2F(0.75f, 0.8f));
        winBanner.SetFont("Stencil");
        winBanner.SetColor(255, 0, 255, 0);
        pointsBanner = new Text(
            pointmanager.PointsToString() + " Points ",
            new Vec2F(0.69f, 0.4f),
            new Vec2F(0.6f, 0.8f));
        pointsBanner.ScaleText(0.5f);
        pointsBanner.SetFont("Stencil");
        pointsBanner.SetColor(255, 255, 255, 255);
    }
    protected override void HandleKeyPressEvent(KeyboardKey key, string _) {
        if (key == KeyboardKey.Enter) {
            Context.SwitchState(new MainMenu());
        }
    }
    protected override void RenderInternal() {
        winBanner.RenderText();
    }
}
