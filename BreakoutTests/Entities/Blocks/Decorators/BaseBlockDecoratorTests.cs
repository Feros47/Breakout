namespace BreakoutTests.Entities.Blocks.Decorators;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using BreakoutTests.Stubs;
using DIKUArcade.Entities;

[TestFixture]
public class BaseBlockDecoratorTests {
    private BaseBlockDecorator decorator;
    private Block decorated;
    [SetUp]
    public void SetUp() {
        decorated = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new ImageStub());
        decorator = new BlockDecoratorStub(decorated);
    }

    [Test]
    public void TestHealth() {
        Assert.True(decorator.Health == decorated.Health);
        var previousHealth = decorated.Health;
        decorator.Health++;
        Assert.That(decorated.Health, Is.EqualTo(previousHealth + 1));
    }
    [Test]
    public void TestValue() {
        Assert.That(decorator.Value, Is.EqualTo(decorated.Value));
    }
    [Test]
    public void TestImage() {
        Assert.That(decorator.Image is ImageStub);
    }
    [Test]
    public void TestShape() {
        var s1 = decorator.Shape;
        var s2 = decorated.Shape;

        Assert.That(
            s1.Position.X == s2.Position.X && s1.Position.Y == s2.Position.Y &&
            s1.Extent.X == s2.Extent.X && s1.Extent.Y == s2.Extent.Y);
    }
    [Test]
    public void TestIsBlockType() {
        // Three partitions:
        // (1) neither are the correct type -> false
        // (2) decorator is the correct type -> true
        // (3) decorated is the correct type -> true
        Assert.False(decorator.IsBlockType<HardenedBlockDecorator>());
        Assert.True(decorator.IsBlockType<BlockDecoratorStub>());
        Assert.True(decorator.IsBlockType<Block>());
    }
    /// <summary>
    /// Empty implementation of a decorator used to check all base-methods.
    /// </summary>
    private class BlockDecoratorStub : BaseBlockDecorator {
        public BlockDecoratorStub(BaseBlock decorated) : base(decorated) { }
    }
}
