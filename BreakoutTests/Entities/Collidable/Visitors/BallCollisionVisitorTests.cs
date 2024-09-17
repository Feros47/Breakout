namespace BreakoutTests.Entities.Collidable.Visitors;

using System.Collections.Generic;
using System.Linq;
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
public class BallCollisionVisitorTests {
    private BallCollisionVisitor ballCollisionVisitor;
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
        ballCollisionVisitor = new();
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

    // black box
    [Test]
    public void TestCollideWithBlock() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUnchecked
        };
        // Precondition
        Assert.That(blocks.All(b => !b.IsDeleted()), Is.True);
        var initialHealthList = blocks
            .Select(block => (block, block.Health))
            .ToList();
        blocks.ForEach(b => ballCollisionVisitor.Collide(b, data));
        // Postcondition that all blocks tat can take damage should.
        var unbreakables = initialHealthList.Where(tuple => tuple.block.IsBlockType<UnbreakableBlockDecorator>());
        var allHaveLostHealth = initialHealthList
            .Except(unbreakables)
            .ToList()
            .All(tuple => tuple.block.Health < tuple.Health);
        Assert.True(allHaveLostHealth);
    }

    // black box
    [Test]
    public void TestPlayerCollision() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirUnchecked
        };
        var prevPlayerPosX = player.GetShape().Direction.X;
        var prevPlayerPosY = player.GetShape().Direction.Y;
        ballCollisionVisitor.Collide(player, data);
        Assert.Multiple(() => {
            Assert.That(prevPlayerPosX, Is.EqualTo(player.GetShape().Direction.X).Within(1e-6));
            Assert.That(prevPlayerPosY, Is.EqualTo(player.GetShape().Direction.Y).Within(1e-6));
            Assert.That(player.IsDeleted(), Is.EqualTo(false));
        });
    }

    //black box
    [Test]
    public void TestCollideWithBall() {
        var data = new CollisionData {
            Collision = true,
            DirectionFactor = new Vec2F(1.0f, 1.0f),
            CollisionDir = CollisionDirection.CollisionDirDown
        };
        var otherBall = new Ball(DIKUArcadeExtensions.LoadImageFromAssets("ball.png"));
        var prevDir = otherBall.GetShape().Direction;
        // Precondition
        Assert.That(otherBall.GetShape().Direction, Is.EqualTo(prevDir));
        ballCollisionVisitor.Collide(otherBall, data);
        // Postcondition
        Assert.That(otherBall.GetShape().Direction, !Is.EqualTo(prevDir));
    }
}
