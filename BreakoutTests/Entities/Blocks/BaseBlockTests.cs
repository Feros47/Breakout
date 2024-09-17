namespace BreakoutTests.Entities.Blocks;

using Breakout.Entities.Blocks;
using Breakout.Entities.Collidable.Visitors;
using BreakoutTests.Stubs;
using DIKUArcade.Physics;

[TestFixture]
public class BaseBlockTests {
    private BaseBlockStub baseBlock;

    [SetUp]
    public void SetUp() {
        baseBlock = new BaseBlockStub();
    }

    [TestCase(true, true)]
    [TestCase(false, false)]
    public void TestShouldIgnore(bool isDeleted, bool expected) {
        baseBlock.Deleted = isDeleted;
        Assert.That(baseBlock.ShouldIgnore(), Is.EqualTo(expected));
    }

    [Test]
    public void TestIsBlockType() {
        Assert.That(baseBlock.IsBlockType<BaseBlock>(), Is.True);
        Assert.That(baseBlock.IsBlockType<Block>(), Is.False);
    }

    [Test]
    public void TestMakeVisitor() {
        Assert.True(baseBlock.MakeVisitor() is BlockCollisionVisitor);
    }

    [Test]
    public void TestAcceptCollision() {
        var visitor = new CollisionVisitorStub();
        var data = new CollisionData();

        try {
            baseBlock.AcceptCollision(visitor, data);
        } catch (StubException e) {
            if (e.Message == $"{nameof(CollisionVisitorStub)}.{nameof(BaseBlock)}") {
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
}
