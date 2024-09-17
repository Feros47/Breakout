namespace BreakoutTests.Levels;

using System;
using System.IO;
using Breakout.Levels;

[TestFixture]
public class LegendDataTests {
    private LegendData data;
    [SetUp]
    public void SetUp() {
        data = new LegendData();
    }

    [Test]
    public void TestFileNotExists() {
        Assert.Throws<FileNotFoundException>(() => data.AddLegend('v', "hfjdbd"));
    }
    [Test]
    public void TestAlreadyExists() {
        data.AddLegend('v', "brown-block.png");
        Assert.Throws<InvalidOperationException>(() => data.AddLegend('v', "teal-block.png"));
    }
    [Test]
    public void TestValidInput() {
        data.AddLegend('v', "brown-block.png");
        Assert.That(data.ContainsKey('v'), Is.True);
    }
}
