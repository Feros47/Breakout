namespace Breakout.Entities;
using Breakout;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

/// <summary>
/// The level timer, which loses the game for the player if it runs out.
/// </summary>
public class Timer : BreakoutEntityBase, IGameEventProcessor {
    private static readonly Vec3I baseColor = new Vec3I(255, 255, 255);
    private static readonly Vec3I doubleSpeedColor = new Vec3I(255, 0, 0);

    private readonly long secondDuration;
    private long timeAtLastUpdate;
    private long timeLeft;
    private Vec2F textExtend;
    private Vec2F textPosition;

    private Text timeDisplay;

    /// <summary>
    /// Create a new timer with the given time limit.
    /// </summary>
    /// <param name="levelTimerInSeconds"></param>
    /// <param name="inGameSecondDurationMs">The number of milliseconds there should pass for a second to pass "in game"</param>
    /// <returns>Timer</returns>
    public Timer(long levelTimerInSeconds, long inGameSecondDurationMs = 1000) : base(
        new StationaryShape(new Vec2F(0.42f, 0.025f), new Vec2F(0.15f, 0.05f)),
        DIKUArcadeExtensions.LoadImageFromAssets("emptyPoint.png")) {
        secondDuration = inGameSecondDurationMs;
        timeAtLastUpdate = StaticTimer.GetElapsedMilliseconds();
        timeLeft = levelTimerInSeconds;

        textExtend = new Vec2F(0.15f, 0.15f);
        textPosition = new Vec2F(0.475f, -0.0859f);
        timeDisplay = new Text(GetTimeDisplay(), textPosition, textExtend);
        timeDisplay.SetColor(baseColor);
        BreakoutBus.GetBus().Subscribe(GameEventType.StatusEvent, this);
    }

    public bool IsDoubleTime {
        get; set;
    }
    /// <summary>
    /// If the time left in the timer less than or equal to zero, the value cannot be set lower
    /// </summary>
    public long TimeLeft {
        get {
            return timeLeft;
        }
        private set {
            if (value <= 0) {
                timeLeft = 0;
                BreakoutBus.GetBus().RegisterEvent(new GameEvent() {
                    EventType = GameEventType.StatusEvent,
                    Message = "TIMEOUT",
                });
            } else {
                timeLeft = value;
            }
        }
    }

    /// <summary>
    /// Render the timer to the screen
    /// </summary>
    public override void Render() {
        var display = GetTimeDisplay();
        timeDisplay.SetText(display);
        timeDisplay.SetColor(
            IsDoubleTime ? doubleSpeedColor : baseColor);
        RenderEntity();
        timeDisplay.RenderText();
    }
    /// <summary>
    /// Update the timers value
    /// </summary>
    public override void Update() {
        DecreaseTimer();
    }

    private bool SecondHasPassed() {
        var second = secondDuration;
        if (IsDoubleTime) {
            second /= 4;
        }
        if (timeAtLastUpdate + second < StaticTimer.GetElapsedMilliseconds()) {
            timeAtLastUpdate = StaticTimer.GetElapsedMilliseconds();
            return true;
        }
        return false;
    }

    private void DecreaseTimer() {
        if (SecondHasPassed()) {
            TimeLeft--;
        }
    }

    private string GetTimeDisplay() {
        var minutes = Math.Max(timeLeft / 60, 0);
        var seconds = Math.Max(timeLeft % 60, 0);
        return $"{minutes}:{(seconds < 10 ? "0" : "")}{seconds}";
    }

    /// <summary>
    /// Process the event
    /// </summary>
    /// <param name="gameEvent"></param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.StatusEvent) {
            switch (gameEvent.Message) {
                case "CLOCK_DOWN":
                    IsDoubleTime = true;
                    break;
                case "CLOCK_NORMAL":
                    IsDoubleTime = false;
                    break;
            }
        }
    }
}