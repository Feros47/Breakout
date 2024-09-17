namespace BreakoutTests.Entities.Blocks;

using Breakout;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Utility;
using BreakoutTests.Stubs;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

// Blackbox testing of the base-block implementation.

[TestFixture]
public class BlockTests : BreakoutTestBase {
    private Block block;
    private int initialHealth;
    [SetUp]
    public void SetUp() {
        block = new Block(
            new StationaryShape(new Vec2F(1.0f, 2.0f), new Vec2F(3.0f, 4.0f)),
            new ImageStub());
        initialHealth = block.Health;
    }

    [Test]
    public void TestInitialValues() {
        Assert.Multiple(() => {
            Assert.That(block.Health, Is.EqualTo(10));
            Assert.That(block.Value, Is.EqualTo(10));
            Assert.Multiple(() => {
                var shape = block.Shape;
                Assert.That(shape.Position.X, Is.EqualTo(1.0f));
                Assert.That(shape.Position.Y, Is.EqualTo(2.0f));
                Assert.That(shape.Extent.X, Is.EqualTo(3.0f));
                Assert.That(shape.Extent.Y, Is.EqualTo(4.0f));
            });
            Assert.That(block.Image, Is.TypeOf<ImageStub>());
        });
    }
    [Test]
    public void TestHandleCollision() {
        // TestIsDeleted removes need for precondition check here.
        var previousHealth = block.Health;
        block.HandleCollision();
        BreakoutBus
            .GetBus()
            .ProcessEventsSequentially();
        Assert.Multiple(() => {
            Assert.That(block.Health, Is.EqualTo(previousHealth - initialHealth));
            Assert.That(block.IsDeleted());
        });
    }
    [Test]
    public void TestRender() {
        try {
            block.Render();
        } catch (StubException e) {
            if (e.Message == $"{nameof(ImageStub)}.Render") {
                Assert.Pass();
            }
        }
        Assert.Fail();
    }
    // There's nothing to test for Update.

    [Test]
    public void TestIsDeleted() {
        Assert.That(block.IsDeleted(), Is.False);
    }

    [TestCase("ROCKET_EXPLOSION", true)]
    [TestCase("test", false)]
    public void TestProcessEvent(string msg, bool isDeletedExpected) {
        var stride = DIKUArcadeExtensions.LoadStridesFromAssets("Explosion.png", 8, 10) as ImageStride;
        var c = new AnimationContainer(1);
        var ev = new GameEvent() {
            EventType = GameEventType.GraphicsEvent,
            Message = msg,
            ObjectArg1 = new AnimationAdapter(
                80,
                new StationaryShape(new Vec2F(1.01f, 2.0f), new Vec2F(3.0f, 4.0f)),
                stride)
        };

        Assert.That(block.IsDeleted(), Is.False);
        block.ProcessEvent(ev);
        Assert.That(block.IsDeleted(), Is.EqualTo(isDeletedExpected));
    }
    private void HandleCollisionAssertion(GameEvent gameEvent) {
        Assert.Multiple(() => {
            Assert.That(gameEvent.EventType, Is.EqualTo(GameEventType.StatusEvent));
            Assert.That(gameEvent.Message, Is.EqualTo("POINTS"));
            Assert.That(gameEvent.IntArg1, Is.EqualTo(block.Value));
        });
    }
}