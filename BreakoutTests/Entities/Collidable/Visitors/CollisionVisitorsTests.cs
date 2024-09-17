namespace BreakoutTests.Entities.Collidable.Visitors;

using DIKUArcade.Math;
using DIKUArcade.Physics;

[TestFixture]
public class CollisionVisitorTest {
    public static bool IsValidBallDirectionChange(CollisionDirection direction, Vec2F previous, Vec2F current) {
        const float EPSILON = 1e-6f;
        switch (direction) {
            case CollisionDirection.CollisionDirUp:
            case CollisionDirection.CollisionDirDown:
                var yExpected = previous.Y * -1;
                return
                    current.Y - yExpected < EPSILON &&
                    current.X - previous.X < EPSILON;
            case CollisionDirection.CollisionDirRight:
            case CollisionDirection.CollisionDirLeft:
                var xExpected = previous.X * -1;
                return
                    current.Y - previous.Y < EPSILON &&
                    current.X - xExpected < EPSILON;
            default:
                return false;
        }
    }

}
