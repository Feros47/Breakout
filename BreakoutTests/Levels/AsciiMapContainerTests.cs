namespace BreakoutTests.Levels;

using System;
using Breakout.Levels;
using DIKUArcade.Math;

[TestFixture]
public class AsciiMapContainerTests {
    private AsciiMapContainer container;
    [SetUp]
    public void SetUp() {
        container = new AsciiMapContainer(new Vec2I(16, 16));
    }

    [Test]
    public void TestOutOfBoundsThrows() {
        // X is too low
        Assert.Throws<ArgumentOutOfRangeException>(() => container.ToDecimalCoordinates(new Vec2I(-1, 8)));
        // X is too large
        Assert.Throws<ArgumentOutOfRangeException>(() => container.ToDecimalCoordinates(new Vec2I(17, 8)));
        // Y is too low
        Assert.Throws<ArgumentOutOfRangeException>(() => container.ToDecimalCoordinates(new Vec2I(8, -1)));
        // Y is too large
        Assert.Throws<ArgumentOutOfRangeException>(() => container.ToDecimalCoordinates(new Vec2I(8, 17)));
        // Both in bounds
        Assert.DoesNotThrow(() => container.ToDecimalCoordinates(new Vec2I(8, 8)));
    }
}
