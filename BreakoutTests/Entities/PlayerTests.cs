namespace BreakoutTests.Entities;
using System.IO;
using Breakout.Entities;
using Breakout.Entities.Collidable.Visitors;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using NUnit.Framework;

[TestFixture]
public class PlayerTests {
    private Player player;
    private float epsilon = 1e-6f;
    [SetUp]
    public void Setup() {
        player = new Player(new Image(Path.Combine("..", "Breakout", "Assets", "Images", "player.png")));
    }

    [Test]
    public void TestIsDynamicShape() {
        Assert.That(player.Shape, Is.AssignableTo<DynamicShape>());
    }

    // Test movement inside of the "bounding" box (i.e. no clamping should happen here)
    [TestCase(0.0f)]
    [TestCase(Player.MOVEMENT_SPEED_INITIAL)]
    [TestCase(-Player.MOVEMENT_SPEED_INITIAL)]
    public void TestUpdate(float xDir) {
        var xInitial = player.Shape.Position.X;
        var yInitial = player.Shape.Position.Y;

        // Precondition
        Assert.That(xInitial, Is.EqualTo(0.45f).Within(epsilon));
        Assert.That(yInitial, Is.EqualTo(0.1f).Within(epsilon));

        player.Shape.AsDynamicShape().Direction = new Vec2F(xDir, 0.0f);
        player.Update();

        Assert.That(player.Shape.Position.X, Is.EqualTo(xInitial + xDir).Within(epsilon));
        Assert.That(player.Shape.Position.Y, Is.EqualTo(yInitial).Within(epsilon));
    }

    [Test]
    public void TestLeftBoundary() {
        player.Shape.Position.X = 0.0f;
        player.Shape.AsDynamicShape().Direction = new Vec2F(-Player.MOVEMENT_SPEED_INITIAL, 0.0f);
        player.Update();

        Assert.That(player.Shape.Position.X, Is.EqualTo(0.0).Within(epsilon));
    }

    [Test]
    public void TestRightBoundary() {
        player.Shape.Position.X = 0.85f;
        player.Shape.AsDynamicShape().Direction = new Vec2F(Player.MOVEMENT_SPEED_INITIAL, 0.0f);
        player.Update();

        Assert.That(player.Shape.Position.X, Is.EqualTo(0.85f).Within(epsilon));
    }

    // Provides 100% C0 as well as C1 coverage
    [TestCase(GameEventType.InputEvent, KeyboardKey.D, false, 0.0f)] // return immediately => no side effects
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.A, true, -Player.MOVEMENT_SPEED_INITIAL)] // Will call SetMoveLeft with key press => moveLeft = -MOVEMENT_SPEED => dir.X = -MOVEMENT_SPEED
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.A, false, 0.0f)] // Will call SetMoveLeft without key press => moveLeft = 0 => dir.X = 0
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.D, true, Player.MOVEMENT_SPEED_INITIAL)] // Will call SetMoveRight with key press => moveRight = MOVEMENT_SPEED => dir.X = MOVEMENT_SPEED
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.D, false, 0.0f)] // Will call SetMoveRight without key press => moveRight = 0 => dir.X = 0
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.N, true, 0.0f)] // Will return => no side effects
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.Space, true, 0.0f)] // Will call ExpendPowerUp with key press => PowerUps.TryPop => PowerUps.Count = 0
    [TestCase(GameEventType.PlayerEvent, KeyboardKey.Space, false, 0.0f)] // Will return => no side effects
    public void TestProcessEvent_C0_C1(GameEventType eventType, KeyboardKey key, bool isKeyPress, float xDirExpected) {
        var gameEvent = new GameEvent {
            EventType = eventType,
            IntArg1 = (int) key,
            Message = isKeyPress ? "KeyPress" : "KeyRelease",
        };
        // Precondition  test.
        Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(0.0f).Within(epsilon));
        Assert.That(player.Shape.AsDynamicShape().Direction.Y, Is.EqualTo(0.0f).Within(epsilon));

        player.ProcessEvent(gameEvent);

        // Postcondition test.
        Assert.That(player.Shape.AsDynamicShape().Direction.X, Is.EqualTo(xDirExpected).Within(epsilon));
        Assert.That(player.Shape.AsDynamicShape().Direction.Y, Is.EqualTo(0.0f).Within(epsilon));
    }

    [Test]
    public void TestReset() {
        player.Shape = new DynamicShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.0f, 0.0f));
        player.ProcessEvent(new GameEvent {
            EventType = GameEventType.PlayerEvent,
            Message = "KeyPress",
            IntArg1 = (int) KeyboardKey.D
        });

        player.Reset();
        Assert.Multiple(() => {
            var shape = player.Shape.AsDynamicShape();
            Assert.That(shape.Position.X == 0.45f);
            Assert.That(shape.Position.Y == 0.1f);
            Assert.That(shape.Direction.X == 0.0f);
            Assert.That(shape.Direction.Y == 0.0f);

            Assert.That(shape.Extent.X == 0.15f);
            Assert.That(shape.Extent.Y == 0.03f);
        });
    }

    [Test]
    public void TestMakeVisitor() {
        Assert.That(player.MakeVisitor(), Is.TypeOf<PlayerCollisionVisitor>());
    }
}

