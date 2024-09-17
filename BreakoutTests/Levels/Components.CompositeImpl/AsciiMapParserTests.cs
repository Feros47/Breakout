namespace BreakoutTests.Levels.Components.CompositeImpl;

using System.Linq;
using Breakout.Exceptions;
using Breakout.Levels;
using Breakout.Levels.Components.CompositeImpl;
using BreakoutTests.Stubs;

[TestFixture]
public class AsciiMapParserTests {
    private AsciiMapParser parser;
    private LevelData level;

    [SetUp]
    public void Setup() {
        parser = new AsciiMapParser();
        level = new LevelData();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("\n")]
    public void TestInvalidInput(string s) {
        Assert.Throws<ParsingException>(() => parser.Parse(s));
    }

    [Test]
    public void TestDifferingRowLengths() {
        var map = "---\n--\n---";
        Assert.Throws<ParsingException>(() => parser.Parse(map));
    }

    [TestCase("---\n---\n---", 0)]
    [TestCase("---\n-x-\n---", 1)]
    [TestCase("x--\n-x-\n--x", 3)]
    public void TestChildrenCount(string map, int expectedChildren) {
        parser.Parse(map);
        Assert.That(parser.Children.Count, Is.EqualTo(expectedChildren));
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
            Assert.That(level.AsciiMap, Is.Not.EqualTo(default(AsciiMapContainer)));
            Assert.That(stubs.All(s => s.PopulateCalled), Is.True);
        });
    }
}
