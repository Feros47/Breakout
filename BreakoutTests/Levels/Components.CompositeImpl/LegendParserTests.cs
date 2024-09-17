namespace BreakoutTests.Levels.Components.CompositeImpl;

using System.Linq;
using Breakout.Levels;
using Breakout.Levels.Components.CompositeImpl;
using Breakout.Levels.Components.LeafImpl;
using BreakoutTests.Stubs;

[TestFixture]
public class LegendDataParserTests {
    private LegendParser parser;
    private LevelData level;

    [SetUp]
    public void SetUp() {
        parser = new LegendParser();
        level = new LevelData();
    }

    [TestCase("", 0)]
    [TestCase("h) xyz", 1)]
    [TestCase("h) xyz\nb) abc", 2)]
    public void TestInputPartitions(string input, int expectedChildCount) {
        parser.Parse(input);
        Assert.Multiple(() => {
            Assert.That(parser.Children.Count, Is.EqualTo(expectedChildCount));
            Assert.That(parser.Children.All(x => x is LegendAttributeParser), Is.True);
        });
    }

    [TestCase(0)]
    [TestCase(1)]
    public void TestPopulate(int childCount) {
        for (var i = 0; i < childCount; i++) {
            parser.Children.Add(new ComponentStub());
        }
        var stubs = parser.Children.Cast<ComponentStub>();
        parser.Populate(level);
        Assert.Multiple(() => {
            Assert.That(level.LegendData, Is.Not.EqualTo(default(LegendData)));
            Assert.That(stubs.All(s => s.PopulateCalled), Is.True);
        });
    }
}
