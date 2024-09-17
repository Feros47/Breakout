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
public class BlockCollisionVisitorTests {
    private BlockCollisionVisitor blockCollisionVisitor;
    private Ball ball;
    private BaseBlock regularBlock;
    private Player player;
    private List<BaseBlock> blocks;

    [SetUp]
    public void SetUp() {
        var ballImg = DIKUArcadeExtensions.LoadImageFromAssets("ball.png");
        var imgp = new LegendData.BlockImagePair("brown-block.png");
        var shape = new StationaryShape(0.3f, 0.3f, 0.3f, 0.3f);

        ball = new Ball(ballImg);
        blockCollisionVisitor = new();
        blocks = new()
        {
            new Block(shape, imgp.HealthyBlockImage),
            new HardenedBlockDecorator(new Block(shape, imgp.HealthyBlockImage), imgp.DamagedBlockImage),
            new PowerUpBlockDecorator(new Block(shape, imgp.HealthyBlockImage), new LaserStrategy()),
            new UnbreakableBlockDecorator(new Block(shape, imgp.HealthyBlockImage))
        };
        regularBlock = new Block(shape, imgp.HealthyBlockImage);
        player = new Player(ballImg);
    }
    [TestCase(CollisionDirection.CollisionDirUp)]
    [TestCase(CollisionDirection.CollisionDirDown)]
    [TestCase(CollisionDirection.CollisionDirLeft)]
    [TestCase(CollisionDirection.CollisionDirRight)]
    public void TestCollideWithBall(CollisionDirection direction) {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = direction
        };
        foreach (var _ in blocks) {
            var prevDirX = ball.Direction.X;
            var prevDirY = ball.Direction.Y;
            blockCollisionVisitor.Collide(ball, data);
            Assert.True(CollisionVisitorTest.IsValidBallDirectionChange(direction, new Vec2F(prevDirX, prevDirY), ball.Direction));
        }
    }

    [Test]
    public void TestCollideWithBlock() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUp
        };
        foreach (var block in blocks) {
            var prevBlockHealth = block.Health;
            var prevBlockValue = block.Value;
            blockCollisionVisitor.Collide(block, data);
            Assert.Multiple(() => {
                Assert.That(block.Health, Is.EqualTo(prevBlockHealth));
                Assert.That(block.Value, Is.EqualTo(prevBlockValue));
                Assert.That(block.IsDeleted(), Is.EqualTo(false));
            });
        }
    }
    [Test]
    public void TestCollideWithPlayer() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUp
        };
        var prevPlayerPosX = player.GetShape().Direction.X;
        var prevPlayerPosY = player.GetShape().Direction.Y;
        blockCollisionVisitor.Collide(player, data);
        Assert.Multiple(() => {
            Assert.That(prevPlayerPosX, Is.EqualTo(player.GetShape().Direction.X).Within(1e-6));
            Assert.That(prevPlayerPosY, Is.EqualTo(player.GetShape().Direction.Y).Within(1e-6));
            Assert.That(player.IsDeleted(), Is.EqualTo(false));
        });
    }

}
