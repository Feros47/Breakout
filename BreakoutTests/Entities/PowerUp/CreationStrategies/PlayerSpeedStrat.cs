namespace BreakoutTests.Entities.PowerUp.CreationStrategies;

using System.Linq;
using Breakout;
using Breakout.Entities;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Managers;
using Breakout.State;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;

[TestFixture]
public class PlayerSpeedStrat : BreakoutTestBase {
    private PlayerSpeedStrategy strategy;

    private GameRunning game;
    //private ConditionalProcessorStub processorStub;
    [SetUp]
    public void SetUp() {
        strategy = new PlayerSpeedStrategy();
        game = new GameRunning(1, new HealthManager(), new PointManager());
        BreakoutBus.ClearEvents();
        //processorStub = new ConditionalProcessorStub();
        //processorStub.Callback = this.Callback;
    }

    // how we intented to use the processor stub
    /*
    private void Callback(GameEvent gameevent) {
        if (gameevent.Message == "POWERUP_EXPEND_PLAYER_INCREASESPEED")
            Assert.Pass();
        Assert.Fail();
    }
    */

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<PlayerSpeedStrategy>());
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
        Assert.That(speedAfter, Is.GreaterThan(speedBefore));
    }

}