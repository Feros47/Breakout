namespace BreakoutTests.Entities.Collidable.Visitors;

using System.Collections.Generic;
using System.IO;
using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.CreationStrategies.PowerUpStrategies;
using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Utilities;

[TestFixture]

public class PowerUpItemVisitorTests {
    private List<BaseBlock> blocks = new();
    private Ball ball;
    private Player player;

    private readonly CollisionData data = new() {
        Collision = true,
        CollisionDir = CollisionDirection.CollisionDirLeft
    };

    [SetUp]
    public void Setup() {
        var shape = new StationaryShape(0.3f, 0.3f, 0.3f, 0.3f);
        var img = new Image(Path.Combine(FileIO.GetProjectPath(), "..", "Breakout", "Assets", "Images", "brown-block.png"));
        var imgp = new LegendData.BlockImagePair("brown-block.png");
        var laser = new LaserStrategy().CreateItem(new Vec2F(0f, 0f));
        var rocket = new RocketStrategy().CreateItem(new Vec2F(0f, 0f));
        var ballItem = new BallSplitStrategy().CreateItem(new Vec2F(0f, 0f));
        var stratList = new List<DropItem>() { laser, rocket, ballItem };
        blocks = new()
        {
            new Block(shape, imgp.HealthyBlockImage),
            new HardenedBlockDecorator(new Block(shape, imgp.HealthyBlockImage), imgp.DamagedBlockImage),
            new PowerUpBlockDecorator(new Block(shape, imgp.HealthyBlockImage), new LaserStrategy()),
            new UnbreakableBlockDecorator(new Block(shape, imgp.HealthyBlockImage))
        };
        ball = new Ball(img);
        player = new Player(img);
    }

    [Test]
    public void PowerUpItemTest() {
    }
}