namespace BreakoutTests.Entities;

using Breakout.Entities;
using DIKUArcade.Events;

[TestFixture]
public class TimerTests {

    [Test]
    public void TestUpdateSecondPassed() {
        var timer = new Timer(100, 0);
        timer.IsDoubleTime = true;
        var previousTime = timer.TimeLeft;

        timer.Update();
        Assert.That(timer.TimeLeft, Is.EqualTo(previousTime - 1));
    }
    [Test]
    public void TestUpdateSecondNotPassed() {
        var timer = new Timer(100, (long) 1e6);
        var previousTime = timer.TimeLeft;

        timer.Update();
        Assert.That(timer.TimeLeft, Is.EqualTo(previousTime));
    }

    [Test]
    public void TestNoTimeLeft() {
        var timer = new Timer(1, 0);
        timer.Update();

        // This is a white-box test, i.e. we know that when entering the "value <= 0" in TimeLeft, the event is raised.
        Assert.That(timer.TimeLeft, Is.EqualTo(0));
    }

    [TestCase(GameEventType.StatusEvent, "CLOCK_DOWN", true)]
    [TestCase(GameEventType.StatusEvent, "CLOCK_NORMAL", false)]
    public void TestProcessEvent(GameEventType eventType, string msg, bool expectedDoubleTime) {
        var timer = new Timer(100);
        timer.IsDoubleTime = !expectedDoubleTime;

        timer.ProcessEvent(new GameEvent {
            EventType = eventType,
            Message = msg
        });
        Assert.That(timer.IsDoubleTime, Is.EqualTo(expectedDoubleTime));
    }

    [TestCase(GameEventType.WindowEvent, "CLOCK_DOWN", true)]
    [TestCase(GameEventType.StatusEvent, "", false)]
    public void TestProcessEventNoFire(GameEventType eventType, string msg, bool timerInitial) {
        var timer = new Timer(100);
        timer.IsDoubleTime = timerInitial;

        timer.ProcessEvent(new GameEvent {
            EventType = eventType,
            Message = msg
        });
        Assert.That(timer.IsDoubleTime, Is.EqualTo(timerInitial));
    }
}
