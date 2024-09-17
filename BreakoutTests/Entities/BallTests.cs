namespace BreakoutTests.Entities;

using System.IO;
using Breakout.Entities;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Utility;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using NUnit.Framework;

[TestFixture]

public class BallTests {
    private Ball ball;
    private readonly float computationalMargin = 0.002f;

    [SetUp]
    public void SetUp() {
        ball = new Ball(new Image(Path.Combine("Assets", "Images", "ball.png")));
    }

    [TestCase(0.0f, 0.0f)]
    [TestCase(0.01f, 0.011f)]
    [TestCase(-0.01f, 0.011f)]
    public void TestBallUpdate(float x, float y) {
        var xInitial = ball.Shape.Position.X;
        var yInitial = ball.Shape.Position.Y;

        Assert.Multiple(() => {
            // Precondition
            Assert.That(xInitial, Is.EqualTo(0.525f).Within(computationalMargin));
            Assert.That(yInitial, Is.EqualTo(0.1f).Within(computationalMargin));
        });

        ball.Shape.AsDynamicShape().Direction = new Vec2F(x, y);
        ball.Update();

        Assert.Multiple(() => {
            Assert.That(ball.Shape.Position.X, Is.EqualTo(xInitial + x).Within(computationalMargin));
            Assert.That(ball.Shape.Position.Y, Is.EqualTo(yInitial + y).Within(computationalMargin));
        });
    }

    [Test]
    public void TestBallBoundary() {
        float direction = 0.2f;
        ball.Shape.Position = new Vec2F(0.97f, 0.97f);
        ball.Shape.AsDynamicShape().Direction.X = direction;
        ball.Shape.AsDynamicShape().Direction.Y = direction;
        ball.Update();
        Assert.Multiple(() => {
            Assert.That(ball.Shape.AsDynamicShape().Direction.X, Is.EqualTo(direction * -1));
            Assert.That(ball.Shape.AsDynamicShape().Direction.Y, Is.EqualTo(direction * -1));
        });
    }
    [Test]
    public void TestCheckBoundaryDeletes() {
        ball.Shape.SetPosition(new Vec2F(0.0f, 0.0f));
        ball.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, 0.0f);
        ball.Update();
        Assert.That(ball.IsDeleted(), Is.True);
    }

    [Test]
    public void TestMakeVisitor() {
        Assert.That(ball.MakeVisitor(), Is.TypeOf<BallCollisionVisitor>());
    }

    [Test]
    public void TestShouldIgnoreFalse() {
        Assert.That(ball.ShouldIgnore(), Is.False);
    }

    [Test]
    public void TestShouldIgnoreTrue() {
        ball.Shape.SetPosition(new Vec2F(0.0f, 0.0f));
        ball.Shape.AsDynamicShape().Direction = new Vec2F(0.0f, 0.0f);

        Assert.That(ball.ShouldIgnore(), Is.False);

        ball.Update();

        Assert.That(ball.ShouldIgnore(), Is.True);
    }

    // Collision visitor tests make sure that there is 100% C1 coverage for all paths except for the one where otherEntitySpeed != null
    // so it is the only case we'll test here to ensure that ChangeDirection has 100% branch coverage.
    [Test]
    public void TestChangeDirection() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUp
        };
        var otherSpeed = new Vec2F(0.0f, 0.0f);
        var prevDirX = ball.Direction.X;
        var prevDirY = ball.Direction.Y;
        ball.ChangeDirection(data, otherSpeed);

        Assert.Multiple(() => {
            Assert.That(ball.Direction.X, Is.EqualTo(prevDirX).Within(1e-6f));
            Assert.That(ball.Direction.Y, Is.EqualTo(-prevDirY).Within(1e-6f));
        });
    }

    [TestCase(1e-5f)]
    [TestCase(-1e-5f)]
    public void TestNearZeroChangeDirection(float xDir) {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUnchecked
        };
        ball.Direction.Y = 0.0f;
        ball.Direction.X = xDir;
        ball.ChangeDirection(data, null);


        var expectedX = (new Vec2F(1e-4f, 0.0f).UnitVector() * ball.Speed).X;

        Assert.That(ball.Direction.X, Is.EqualTo(expectedX));
    }
}