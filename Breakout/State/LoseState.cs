namespace Breakout.State;

using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using Managers;

/// <summary>
/// The state representing a lost game
/// </summary>
public class LoseState : MenuState {
    private readonly Text loseBanner;
    private readonly Text pointsBanner;
    public LoseState(PointManager pointsmanager) : base(new[] { "Menu" }) {
        loseBanner = new Text(
            "GAME OVER",
            new Vec2F(0.2f, 0.0f),
            new Vec2F(0.60f, 0.8f));
        loseBanner.SetFont("Stencil");
        loseBanner.SetColor(255, 255, 0, 0);
        pointsBanner = new Text(
            pointsmanager.PointsToString() + " Points ",
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
        loseBanner.RenderText();
        pointsBanner.RenderText();
    }
}
