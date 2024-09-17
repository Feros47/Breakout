namespace BreakoutTests.Levels;

using System;
using Breakout.Levels;

[TestFixture]
public class MetadataTests {
    private Metadata data;
    [SetUp]
    public void SetUp() {
        data = new Metadata();
    }

    [Test]
    public void TestContainsKeyExists() {
        data["hello"] = "world";
        Assert.True(data.ContainsKey("hello"));
    }
    [Test]
    public void TestContainsKeyNull() {
        Assert.False(data.ContainsKey("hello"));
    }

    [Test]
    public void TestDuplicateWriteThrows() {
        var m = new Metadata();
        Assert.DoesNotThrow(() => m["X"] = "Y");
        Assert.Throws<InvalidOperationException>(() => m["X"] = "Z");
    }
}
