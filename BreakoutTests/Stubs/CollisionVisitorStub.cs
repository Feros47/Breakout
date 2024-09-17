namespace BreakoutTests.Stubs;

using Breakout.Entities;
using Breakout.Entities.Blocks;
using Breakout.Entities.Blocks.Decorators;
using Breakout.Entities.Collidable;
using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.PowerUps;
using DIKUArcade.Physics;

public class CollisionVisitorStub : ICollisionVisitor {
    public void Collide(BaseBlock component, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(BaseBlock)}");
    }
    public void Collide(MovingBlockDecorator player, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(MovingBlockDecorator)}");
    }
    public void Collide(Player player, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(Player)}");
    }
    public void Collide(Ball ball, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(Ball)}");
    }
    public void Collide(DropItem powerUpItem, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(DropItem)}");
    }
    public void Collide(PowerUpExpendLaser powerUpLaser, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(PowerUpExpendLaser)}");
    }
    public void Collide(PowerUpExpendRocket powerUpTNT, CollisionData collisionData) {
        throw new StubException($"{nameof(CollisionVisitorStub)}.{nameof(PowerUpExpendRocket)}");
    }
}
