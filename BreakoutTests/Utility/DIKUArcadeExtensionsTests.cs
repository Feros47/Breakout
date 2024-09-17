namespace BreakoutTests.Utility;

using System;
using System.IO;
using Breakout.Utility;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;

[TestFixture]
public class DIKUArcadeExtensionsTests {

    [TestCase("KeyPress", KeyboardAction.KeyPress)]
    [TestCase("KeyRelease", KeyboardAction.KeyRelease)]
    public void TestActionTypeValidInput(string message, KeyboardAction expectedReturnValue) {
        var ev = new GameEvent { Message = message };
        Assert.That(ev.ActionType(), Is.EqualTo(expectedReturnValue));
    }
    [Test]
    public void TestActionTypeInvalidInput() {
        var ev = new GameEvent { Message = "test" };
        Assert.Throws<ArgumentOutOfRangeException>(() => ev.ActionType());
    }

    [Test]
    public void TestLoadImageFromAssetsValidInput() {
        Assert.DoesNotThrow(() => DIKUArcadeExtensions.LoadImageFromAssets("ball.png"));
    }
    [Test]
    public void TestLoadImageFromAssetsInvalidInput() {
        Assert.Throws<FileNotFoundException>(() => DIKUArcadeExtensions.LoadImageFromAssets(new Guid().ToString()));
    }
    [Test]
    public void TestLoadStridesFromAssetsValidInput() {
        Assert.DoesNotThrow(() => DIKUArcadeExtensions.LoadStridesFromAssets("ball.png", 4));
    }
    [Test]
    public void TestLoadStridesFromAssetsInvalidInput() {
        Assert.Throws<FileNotFoundException>(() => DIKUArcadeExtensions.LoadStridesFromAssets(new Guid().ToString(), 4));
    }

    [TestCase(0.0f, 0.0f)] // vector of length 0
    [TestCase(10.0f, 0.0f)] // vector along positive x-axis
    [TestCase(0.0f, 10.0f)] // vector along positive y-axis
    [TestCase(-10.0f, 0.0f)] // vector along negative x-axis
    [TestCase(0.0f, -10.0f)] // vector along negative y-axis
    [TestCase(10.0f, 10.0f)] // vector in Q1
    [TestCase(0.0f, 0.0f)] // vector in Q2
    [TestCase(0.0f, 0.0f)] // vector in Q3
    [TestCase(0.0f, 0.0f)] // vector in Q4
    public void TestUnitVectorBlackBox(float x, float y) {
        const float EPSILON = 1e-6f;
        var vec = new Vec2F(x, y);
        var length = (float) vec.Length();

        var unit = vec.UnitVector();
        var extended = unit * length;
        Assert.Multiple(() => {
            // Vector hasn't been changed
            Assert.That(vec.X, Is.EqualTo(x).Within(EPSILON));
            Assert.That(vec.Y, Is.EqualTo(y).Within(EPSILON));

            // Expected vector is returned
            Assert.That(extended.X, Is.EqualTo(x).Within(EPSILON));
            Assert.That(extended.Y, Is.EqualTo(y).Within(EPSILON));
        });

    }
}
