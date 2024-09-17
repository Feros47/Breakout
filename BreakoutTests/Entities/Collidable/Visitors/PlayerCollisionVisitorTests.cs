namespace BreakoutTests.Entities.Collidable.Visitors;
using System.Collections.Generic;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Collidable.Visitors;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Levels;
using Breakout.Utility;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Physics;

[TestFixture]
public class PlayerCollisionVisitorTests {
    private PlayerCollisionVisitor playerCollisionVisitor;
    private Ball ball;
    private BaseBlock regularBlock;
    private Player player;
    private List<BaseBlock> blocks;

    [SetUp]
    public void SetUp() {
        var img = DIKUArcadeExtensions.LoadImageFromAssets("ball.png");
        var imgp = new LegendData.BlockImagePair("brown-block.png");
        var shape = new StationaryShape(0.3f, 0.3f, 0.3f, 0.3f);

        ball = new Ball(img);
        playerCollisionVisitor = new PlayerCollisionVisitor(new Player(img));
        blocks = new()
        {
            new Block(shape, imgp.HealthyBlockImage),
            new HardenedBlockDecorator(new Block(shape, imgp.HealthyBlockImage), imgp.DamagedBlockImage),
            new PowerUpBlockDecorator(new Block(shape, imgp.HealthyBlockImage), new LaserStrategy()),
            new UnbreakableBlockDecorator(new Block(shape, imgp.HealthyBlockImage))
        };
        regularBlock = new Block(shape, imgp.HealthyBlockImage);
        player = new Player(img);
    }

    [TestCase(CollisionDirection.CollisionDirDown)]
    [TestCase(CollisionDirection.CollisionDirUp)]
    [TestCase(CollisionDirection.CollisionDirLeft)]
    [TestCase(CollisionDirection.CollisionDirRight)]
    public void TestBallCollision(CollisionDirection direction) {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = direction
        };
        var prevDirX = ball.Direction.X;
        var prevDirY = ball.Direction.Y;
        playerCollisionVisitor.Collide(ball, data);
        Assert.True(CollisionVisitorTest.IsValidBallDirectionChange(direction, new Vec2F(prevDirX, prevDirY), ball.Direction));
    }

    [Test]
    public void TestBlockCollision() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirLeft
        };

        foreach (var block in blocks) {
            var prevBlockHealth = block.Health;
            var prevBlockValue = block.Value;
            playerCollisionVisitor.Collide(block, data);
            Assert.Multiple(() => {
                Assert.That(block.Health, Is.EqualTo(prevBlockHealth));
                Assert.That(block.Value, Is.EqualTo(prevBlockValue));
                Assert.That(block.IsDeleted(), Is.EqualTo(false));
            });
        }
    }

    [Test]
    public void TestPowerUpItemCollision() {
        var pii = new LaserStrategy()
            .CreateItem(new Vec2F(0.5f, 0.5f));
        Assert.False(pii.IsDeleted());
        playerCollisionVisitor.Collide(pii, new CollisionData());
        Assert.True(pii.IsDeleted());
    }
}
