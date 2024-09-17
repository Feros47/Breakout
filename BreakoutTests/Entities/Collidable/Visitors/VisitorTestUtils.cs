namespace BreakoutTests.Entities.Collidable;

using System.Collections.Generic;
using System.Linq;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Collidable;
using DIKUArcade.Physics;
using NUnit.Framework;

public static class VisitorTestUtils {
    public static void AssertPreconditionForBlocks(List<BaseBlock> blocks, int blockHealth) {
        foreach (var block in blocks.Where(b => !b.IsBlockType<UnbreakableBlockDecorator>())) {
            Assert.That(block.IsDeleted(), Is.False);
            Assert.That(block.Health, Is.EqualTo(blockHealth));
        }
    }

    public static void VisitBlocks(ICollisionVisitor collisionVisitor, CollisionData collisionData, List<Block> blocks) {
        foreach (var block in blocks.Where(b => !b.IsBlockType<UnbreakableBlockDecorator>())) {
            collisionVisitor.Collide(block, collisionData);
        }
    }
}