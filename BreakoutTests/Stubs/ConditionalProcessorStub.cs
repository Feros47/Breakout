namespace BreakoutTests.Stubs;

using Breakout;
using DIKUArcade.Events;

public class ConditionalProcessorStub : IGameEventProcessor {
    public ConditionalProcessorStub() {
        foreach (var type in Program.GameEventTypes) {
            BreakoutBus
                .GetBus()
                .Subscribe(type, this);
        }
        Callback = (GameEvent _) => { };
    }
    public delegate void EventDataCheck(GameEvent gameEvent);
    public EventDataCheck Callback {
        get; set;
    }
    public void ProcessEvent(GameEvent gameEvent) {
        Callback(gameEvent);
    }
}
