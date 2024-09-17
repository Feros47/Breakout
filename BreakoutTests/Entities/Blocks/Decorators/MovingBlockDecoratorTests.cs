namespace BreakoutTests.Entities.Blocks.Decorators;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using BreakoutTests.Stubs;
using DIKUArcade.Entities;
using DIKUArcade.Physics;

[TestFixture]
public class MovingBlockDecoratorTests {
    private BaseBlock decorated;
    private MovingBlockDecorator decorator;
    [SetUp]
    public void SetUp() {
        decorated = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new ImageStub());
        decorator = new MovingBlockDecorator(decorated, 1.0f);
    }

    [Test]
    public void TestAcceptCollision() {
        var visitor = new CollisionVisitorStub();
        var data = new CollisionData();
        try {
            decorator.AcceptCollision(visitor, data);
        } catch (StubException e) {
            if (e.Message == $"{nameof(CollisionVisitorStub)}.{nameof(MovingBlockDecorator)}") {
                Assert.Pass();
            }
        }
        Assert.Fail();
    }

    [TestCase(CollisionDirection.CollisionDirLeft, -1.0f)]
    [TestCase(CollisionDirection.CollisionDirRight, -1.0f)]
    [TestCase(CollisionDirection.CollisionDirUp, 1.0f)]
    [TestCase(CollisionDirection.CollisionDirDown, 1.0f)]
    public void TestChangeDirection(CollisionDirection colDir, float expectedMult) {
        var previousDirection = decorator.Direction.X;
        decorator.ChangeDirection(colDir);
        Assert.That(decorator.Direction.X, Is.EqualTo(expectedMult * previousDirection));
    }

    [Test]
    public void TestNoMoveWhenHaveBeenHit() {
        var previousX = decorator.Position.X;
        decorator.ChangeDirection(CollisionDirection.CollisionDirLeft);
        decorator.Update();

        Assert.That(decorator.Position.X, Is.EqualTo(previousX));
    }
    [Test]
    public void TestMoveWhenHaveNotBeenHit() {
        // Test that move is only called when we have been hit.
        var previousX = decorator.Position.X;
        decorator.Update();

        Assert.That(decorator.Position.X, Is.Not.EqualTo(previousX));
    }
}
