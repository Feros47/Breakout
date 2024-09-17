namespace Breakout.Entities;

using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Math;

/// <summary>
/// HealthUI is a UI element that represents the health of the player.
/// </summary>
public class HealthUI : BreakoutEntityBase {

    /// <summary>
    /// Constructor for HealthUI.
    /// </summary>
    /// <param name="vec">Position of the HealthUI</param>
    /// <returns>HealthUI</returns>
    public HealthUI(Vec2F vec) : base(new DynamicShape(vec, new Vec2F(0.07f, 0.07f)), DIKUArcadeExtensions.LoadImageFromAssets("heart_Filled.png")) {
    }

    /// <summary>
    /// Render the HealthUI.
    /// </summary>
    public override void Render() {
        RenderEntity();
    }

    /// <summary>
    /// Update the HealthUI.
    /// </summary>
    public override void Update() {
    }
}