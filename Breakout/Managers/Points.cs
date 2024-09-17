namespace Breakout.Managers;

using Breakout.Entities;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// PointManager is responsible for keeping track of the player's points, and rendering them.
/// </summary>
public class PointManager : BreakoutEntityBase, IGameEventProcessor {

    public int points;
    private Text text;

    /// <summary>
    /// Constructor for PointManager.
    /// </summary>
    /// <returns>PointManager</returns>
    public PointManager() : base(new DynamicShape(new Vec2F(0.8f, 0.025f), new Vec2F(0.15f, 0.05f)), DIKUArcadeExtensions.LoadImageFromAssets("emptyPoint.png")) {
        text = new Text("string", new Vec2F(0.835f, -0.0835f), new Vec2F(0.15f, 0.15f));
        text.SetColor(new Vec3I(255, 255, 255));
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        points = 0;
    }

    /// <summary>
    /// Render the points.
    /// </summary>
    public override void Render() {
        text.SetText(PointsToString());
        RenderEntity();
        text.RenderText();
    }

    public override void Update() {
        // Intentional no-op
    }

    /// <summary>
    /// Add points to the player's total points.
    /// </summary>
    public void AddPoints(int value) {
        points += value;
    }

    /// <summary>
    /// Process the game event.
    /// </summary>
    /// <param name="gameEvent"></param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent && gameEvent.Message == "POINTS") {
            var value = gameEvent.IntArg1;
            AddPoints(value);
        }
    }

    /// <summary>
    /// Converts the points (int) to a string.
    /// </summary>
    /// <returns>
    /// The points as a string.
    /// </returns>
    public string PointsToString() {
        return points.ToString();
    }
}