namespace Breakout;
using System.Linq;
using DIKUArcade.Events;

/// <summary>
/// Singleton holder f0r GameEventBus
/// </summary>
public class BreakoutBus {
    private static class GameEventBusHolder {
        public static GameEventBus instance = new();
    }
    public static GameEventBus GetBus() {
        return GameEventBusHolder.instance;
    }
    /// <summary>
    /// Clear all events registered evens in GameEventBus
    /// </summary>
    public static void ClearEvents() {
        GameEventBusHolder
            .instance
            .Flush();
        var timedEventTypes = Enum
            .GetValues<TimedEventType>()
            .ToList();

        timedEventTypes.ForEach(x => {
            GameEventBusHolder
                .instance
                .CancelTimedEvent((uint) x);
        });
        GameEventBusHolder
            .instance
            .ResetBreakProcessing();
    }

    /// <summary>
    /// NOTE: Only use this method for tests since it will clear EVERYTHING including registered processors.
    /// </summary>
    public static void ResetInstance(List<GameEventType> eventTypesToInit) {
        GameEventBusHolder.instance = new();
        GameEventBusHolder
            .instance
            .InitializeEventBus(eventTypesToInit);
    }
}