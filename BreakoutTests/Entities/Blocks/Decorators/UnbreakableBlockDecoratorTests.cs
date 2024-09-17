namespace BreakoutTests.Entities.Blocks.Decorators;

using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Collidable.Visitors;
using BreakoutTests.Stubs;
using DIKUArcade.Physics;

[TestFixture]
public class UnbreakableBlockDecoratorTests {
    private UnbreakableBlockDecorator decorator;
    private BaseBlockStub decorated;

    [SetUp]
    public void Setup() {
        decorated = new BaseBlockStub();
        decorator = new UnbreakableBlockDecorator(decorated);
    }

    [Test]
    public void TestCollisionByHardBallDeletes() {
        var visitor = new HardBallCollisionVisitor();
        var data = new CollisionData();
        Assert.That(decorator.IsDeleted(), Is.False);
        decorator.AcceptCollision(visitor, data);
        Assert.That(decorator.IsDeleted(), Is.True);
    }

    [Test]
    public void TestHandleCollision() {
        // If the call doesn't throw, BaseBlockStub.HandleCollision hasn't been called.
        Assert.DoesNotThrow(decorator.HandleCollision);
    }
}
