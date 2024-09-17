namespace BreakoutTests.Entities.PowerUp;

using Breakout.Entities.Collidable.Visitors;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using NUnit.Framework;

[TestFixture]
public class HardBall {
    WindowArgs windowArgs;
    PowerUpExpendHardBall powerUp;

    [SetUp]
    public void SetUp() {
        windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        powerUp = new();
    }

    [Test]
    public void TestPowerUpDirection() {
        Assert.That(powerUp.Direction.Y, Is.GreaterThan(0f));
    }

    [Test]
    public void TestCollision() {
        Assert.That(powerUp.MakeVisitor(), Is.TypeOf<HardBallCollisionVisitor>());
    }

    [Test]
    public void TestAcceptCollision() {
        powerUp.GetShape().SetPosition(new Vec2F(0.5f, 0.5f));
        var direction = powerUp.GetShape().Direction.X;
        powerUp.AcceptCollision(new BlockCollisionVisitor(), new CollisionData() {
            Collision = true,
            CollisionDir = CollisionDirection.CollisionDirLeft
        });
        Assert.That(powerUp.GetShape().Direction.X, Is.EqualTo(-direction).Within(1e-4f));
    }
}