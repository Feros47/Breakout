namespace BreakoutTests;

using System;
using Breakout;
using Breakout.State;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;

[TestFixture]
public class GameContextTests {
    private GameContext gameContext;

    [SetUp]
    public void Setup() {
        gameContext = GameContext.BuildContext<MainMenu>(new WindowArgs() { Title = "Breakout v0.1" });
    }

    [Test]
    public void TestSwitchState() {
        var newState = new TestState();
        // Precondition
        Assert.False(newState.HasBeenReset);
        Assert.That(newState.GetContext(), Is.EqualTo(default(GameContext)));

        gameContext.SwitchState(newState);

        // PostCondition
        Assert.Multiple(() => {
            Assert.That(gameContext.ActiveState, Is.EqualTo(newState));
            Assert.That(newState.GetContext(), Is.EqualTo(gameContext));
            Assert.That(newState.HasBeenReset, Is.True);
        });
    }

    [Test]
    public void TestRenderIsDeferred() {
        gameContext.SwitchState(new TestState());
        var state = (gameContext.ActiveState as TestState)!;

        Assert.Multiple(() => {
            Assert.That(state.HasBeenUpdated, Is.False);
            Assert.That(state.HasBeenRendered, Is.False);
        });
        gameContext.Render();
        Assert.Multiple(() => {
            Assert.That(state.HasBeenUpdated, Is.False);
            Assert.That(state.HasBeenRendered, Is.True);
        });
    }

    // Singleton global state makes this test pass when run by itself, but fail when run along with all others.
    /*[Test]
    public void TestUpdateIsDeferred() {
        gameContext.SwitchState(new TestState());
        var state = (gameContext.ActiveState as TestState)!;

        Assert.Multiple(() => {
            Assert.That(state.HasBeenUpdated, Is.False);
            Assert.That(state.HasBeenRendered, Is.False);
        });
        try {

            gameContext.Update();
        } catch (StubException) { }
        Assert.Multiple(() => {
            Assert.That(state.HasBeenUpdated, Is.True);
            Assert.That(state.HasBeenRendered, Is.False);
        });
    }*/

    [Test]
    public void TestProcessEvent() {
        gameContext.ProcessEvent(new GameEvent {
            EventType = GameEventType.ControlEvent,
            Message = "APPLICATION_STOP"
        });
        Assert.That(gameContext.IsRunning, Is.False);
    }

    [Test]
    public void TestProcessEventDirectReturn() {
        gameContext.ProcessEvent(new GameEvent {
            EventType = GameEventType.GraphicsEvent,
            Message = "APPLICATION_STOP"
        });
        Assert.That(gameContext.IsRunning, Is.True);
    }

    /// <summary>
    /// Defines a "state-template" which lets us test internals of the state pattern implementation.
    /// </summary>
    private class TestState : BreakoutState {
        public bool HasBeenReset {
            get; private set;
        } = false;
        public bool HasBeenUpdated {
            get; private set;
        } = false;
        public bool HasBeenRendered {
            get; private set;
        } = false;
        public GameContext GetContext() {
            return Context;
        }

        // handleevent is difficult to test since we would need access to the window object itself, so
        // it is not tested.
        public override void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            throw new NotImplementedException();
        }
        public override void RenderState() {
            HasBeenRendered = true;
        }
        public override void ResetState() {
            HasBeenReset = true;
        }
        public override void UpdateState() {
            HasBeenUpdated = true;
        }
    }
}
