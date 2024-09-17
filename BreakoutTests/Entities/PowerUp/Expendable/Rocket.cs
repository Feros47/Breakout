namespace BreakoutTests.Entities.PowerUp;

using Breakout.Entities.Blocks;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Physics;
using NUnit.Framework;

[TestFixture]
public class Rocket {
    WindowArgs windowArgs;
    PowerUpExpendRocket powerUp;

    [SetUp]
    public void SetUp() {

        windowArgs = new WindowArgs() { Title = "Breakout v0.1" };
        powerUp = new();
    }

    [Test]
    public void TestPowerUpDirection() {
        var prevX = powerUp.Shape.Position.X;
        var prevY = powerUp.Shape.Position.Y;
        powerUp.Update();
        Assert.That(prevX, Is.EqualTo(powerUp.Shape.Position.X));
        Assert.That(prevY, Is.LessThan(powerUp.Shape.Position.Y));
    }

    [Test]
    public void TestIsDeleted() {
        Assert.That(powerUp.IsDeleted(), Is.False);
        Assert.That(powerUp.ShouldIgnore(), Is.False);
        powerUp.Shape.Position.Y = 1.2f;
        powerUp.Update();
        Assert.That(powerUp.IsDeleted(), Is.True);
        Assert.That(powerUp.IsDeleted(), Is.True);
    }
    [Test]
    public void TestCollision() {
        Assert.That(powerUp.MakeVisitor(), Is.TypeOf<PowerUpExpendCollisionVisitor>());
    }
    [Test]
    public void TestAcceptCollision() {
        powerUp.AcceptCollision(new BlockCollisionVisitor(), new CollisionData() { Collision = true });
        Assert.That(powerUp.IsDeleted(), Is.True);
    }

    [Test]
    public void TestDestroyBlock() {
        BaseBlock block = new Block(new StationaryShape(0f, 0f, 0f, 0f), new NoImage());
        Assert.That(block.IsDeleted(), Is.False);
        PowerUpExpendCollisionVisitor visitor = new();
        visitor.Collide(block, new CollisionData() { Collision = true });
        Assert.That(block.IsDeleted(), Is.True);
    }
}