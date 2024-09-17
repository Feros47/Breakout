namespace BreakoutTests.State;

using System.Collections.Generic;
using Breakout;
using Breakout.Managers;
using Breakout.State;

[TestFixture]
public class BreakoutStateTests {
    private List<BreakoutState> breakoutStates;
    private GameContext gameContext;

    [SetUp]
    public void SetUp() {
        gameContext = GameContext.BuildContext<MainMenu>(new DIKUArcade.GUI.WindowArgs { Title = "Breakout v0.1" });
        breakoutStates = new() {
            new GamePaused(new GameRunning(1, new HealthManager(), new PointManager())),
            new GameRunning(1, new HealthManager(), new PointManager()),
            new LoseState(new PointManager()),
            new MainMenu(),
            new WinState(new PointManager())
        };
    }

    [Test]
    public void TestStateSetContext() {
        foreach (var breakoutState in breakoutStates) {
            Assert.That(breakoutState.Context, Is.EqualTo(default(GameContext)));
            breakoutState.SetContext(gameContext);
            Assert.That(breakoutState.Context, Is.EqualTo(gameContext));
        }
    }
}
