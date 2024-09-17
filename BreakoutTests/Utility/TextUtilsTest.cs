namespace BreakoutTests.Utility;

using System;
using Breakout.Utility;

[TestFixture]
public class TextUtilsTest {

    [TestCase("hello; world", ';', "hello", "world")]
    [TestCase("hello: world", ':', "hello", "world")]
    public void TestSupportsDifferentSeparators(string input, char separator, string expectedKey, string expectedValue) {
        Assert.DoesNotThrow(() => input.SplitOnFirstAndTrim(separator));
        var kvp = input.SplitOnFirstAndTrim(separator);

        Assert.That(
            kvp.Item1, Is.EqualTo(expectedKey));
        Assert.That(
            kvp.Item2, Is.EqualTo(expectedValue));
    }

    [TestCase("hello; world", ';', "hello", "world")]
    [TestCase("  hello ; world  ", ';', "hello", "world")]
    [TestCase(";hello world", ';', "", "hello world")]
    [TestCase("hello world;", ';', "hello world", "")]
    public void TestMultiplePositions(string input, char separator, string expectedKey, string expectedValue) {
        Assert.DoesNotThrow(() => input.SplitOnFirstAndTrim(separator));
        var kvp = input.SplitOnFirstAndTrim(separator);

        Assert.That(
            kvp.Item1, Is.EqualTo(expectedKey));
        Assert.That(
            kvp.Item2, Is.EqualTo(expectedValue));
    }
    [Test]
    public void TestMultipleSeparators() {
        var kvp = "hello; w;orld".SplitOnFirstAndTrim(';');

        Assert.That(
            kvp.Item1, Is.EqualTo("hello"));
        Assert.That(
            kvp.Item2, Is.EqualTo("w;orld"));
    }
    [Test]
    public void TestThrowsOnMissingSeparator() {
        Assert.Throws<ArgumentOutOfRangeException>(() => "hello world".SplitOnFirstAndTrim(';'));
    }
}
