namespace Breakout.Managers;
using DIKUArcade.Events;

/// <summary>
/// HealthManager is responsible for managing the player's health.
/// </summary>
public class HealthManager : IGameEventProcessor {
    public int Health {
        get; private set;
    }

    /// <summary>
    /// Constructor for HealthManager.
    /// </summary>
    /// <returns>HealthManager</returns>
    public HealthManager() {
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
        Health = 3;
    }

    /// <summary>
    /// Decrease the player's health by 1.
    /// </summary>
    public void DecreaseHealth() {
        Health -= 1;
    }

    /// <summary>
    /// Increase the player's health by 1.
    /// </summary>
    public void IncreaseHealth() {
        Health += 1;
    }

    /// <summary>
    /// Process the game event.
    /// </summary>
    /// <param name="gameEvent"></param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            if (gameEvent.Message == "INCREASEHEALTH") {
                IncreaseHealth();
            } else if (gameEvent.Message == "DECREASEHEALTH") {
                DecreaseHealth();
            }

        }

    }
}