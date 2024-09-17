namespace Breakout;

using Breakout.State;
using DIKUArcade.Events;
using DIKUArcade.GUI;

/// <summary>
/// The class representing the program itself, this is the first class run by the program'
/// This is also the class responsible for creating the BreakoutBus and all of the GameEventTypes
/// </summary>
public static class Program {
#pragma warning disable IDE1006 // Naming Styles
    public static readonly IReadOnlyList<GameEventType> GameEventTypes = new List<GameEventType>() {
#pragma warning restore IDE1006 // Naming Styles
        GameEventType.ControlEvent,
        GameEventType.InputEvent,
        GameEventType.PlayerEvent,
        GameEventType.GraphicsEvent,
        GameEventType.StatusEvent
    };
    static void Main(string[] args) {
        BreakoutBus
            .GetBus()
            .InitializeEventBus(GameEventTypes.ToList());
        var windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        var game = GameContext.BuildContext<MainMenu>(windowArgs);
        game.Run();
    }
}