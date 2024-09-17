namespace BreakoutTests.Entities.Blocks.Decorators;

using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using BreakoutTests.Stubs;

public class HardenedBlockDecoratorTests {
    private HardenedBlockDecorator decorator;
    private BaseBlock decorated;
    private ImageStub damagedImg;

    [SetUp]
    public void Setup() {
        damagedImg = new ImageStub();
        //decorated = new Block(new StationaryShape(0.0f, 0.0f, 0.0f, 0.0f), new ImageStub());
        decorated = new BaseBlockStub();
        decorator = new HardenedBlockDecorator(decorated, damagedImg);
    }

    [Test]
    public void TestHandleCollision() {
        var expected = decorator.Health / 2;
        decorator.HandleCollision();
        Assert.That(decorator.Health == expected);
        Assert.Throws<StubException>(() => decorator.HandleCollision(), "HANDLECOLLISION");
    }

    [Test]
    public void TestRenderFullHealth() {
        Assert.Throws<StubException>(() => decorator.Render(), "RENDER_BASEBLOCK");
    }

    [Test]
    public void TestRenderHalfHealth() {
        decorator.HandleCollision();
        Assert.Throws<StubException>(() => decorator.Render(), "IMAGE");
    }

}
