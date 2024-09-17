namespace BreakoutTests.Entities.Hazards;

using System.Linq;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;

public class DecreaseSpeedItemTests : BreakoutTestBase {

    private DecreasePlayerSpeedStrategy strategy;
    private GameRunning game;
    private HealthManager healthManager;

    [SetUp]
    public void SetUp() {
        strategy = new();
        healthManager = new HealthManager();
        game = new GameRunning(1, healthManager, new PointManager());
    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<DecreasePlayerSpeedStrategy>());
    }


    [Test]
    public void PlayerSpeedChanged() {
        BreakoutBus.GetBus().RegisterEvent(new GameEvent() {
            EventType = GameEventType.InputEvent,
            Message = "KEY_PRESS",
            IntArg1 = (int) KeyboardKey.A,
        });
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var speedBefore = game.GetEntities()
            .OfType<Player>()
            .FirstOrDefault()!
            .MovementSpeed;
        strategy.CreateExpendable(new Vec2F(0f, 0f));
        BreakoutBus.GetBus().ProcessEventsSequentially();
        var speedAfter = game.GetEntities()
            .OfType<Player>()
            .FirstOrDefault()!
            .MovementSpeed;
        Assert.That(speedAfter, Is.LessThan(speedBefore));
    }
}