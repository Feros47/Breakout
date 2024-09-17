namespace BreakoutTests.Levels.Components.CompositeImpl;

using System.Linq;
using Breakout.Exceptions;
using Breakout.Levels;
using Breakout.Levels.Components.CompositeImpl;
using Breakout.Levels.Components.LeafImpl;
using BreakoutTests.Stubs;

[TestFixture]
public class MetadataAttributeParserTests {
    private MetadataAttributeParser parser;
    private LevelData level;
    [SetUp]
    public void SetUp() {
        parser = new();
        level = new LevelData() {
            Metadata = new Metadata()
        };
    }
    [TestCase("")]
    [TestCase("hello) world")]
    public void TestNoSeparator(string tc) {
        Assert.Throws<ParsingException>(() => parser.Parse(tc));
    }

    [Test]
    public void TestKeyValue() {
        parser.Parse(" heLlo : World : ");
        Assert.Multiple(() => {
            Assert.That(parser.Key, Is.EqualTo("heLlo"));
            Assert.That(parser.Value, Is.EqualTo("World :"));
            Assert.That(parser.Children.Count, Is.EqualTo(0));
        });
    }

    [TestCase("Hardened")]
    [TestCase("Hazard")]
    [TestCase("Moving")]
    [TestCase("PowerUp")]
    [TestCase("Unbreakable")]
    public void TestIsBlockTypeAttribute(string blockType) {
        parser.Parse($"{blockType}: x");

        Assert.Multiple(() => {
            Assert.That(parser.Key, Is.EqualTo(blockType));
            Assert.That(parser.Value, Is.EqualTo("x"));
            Assert.That(parser.Children.Count, Is.EqualTo(1));
        });
    }

    [Test]
    public void TestSpecialBlockMultiple() {
        parser.Parse("Hardened: x,y");

        Assert.Multiple(() => {
            Assert.That(parser.Children.Count, Is.EqualTo(2));
            Assert.That(parser.Children.All(c => c is BlockTypeAttributeParser), Is.True);
        });
    }

    [Test]
    public void TestPopulate() {
        parser.Parse("hello: world");

        Assert.That(level.Metadata.ContainsKey("hello"), Is.False);
        parser.Populate(level);
        Assert.That(level.Metadata.ContainsKey("hello"), Is.True);
    }

    [Test]
    public void TestPopulateCallsChildren() {
        parser.Parse("hello: world");
        parser.Children.Add(new ComponentStub());
        var stubs = parser.Children.Cast<ComponentStub>();

        parser.Populate(level);
        Assert.That(stubs.All(s => s.PopulateCalled), Is.True);
    }

    [Test]
    public void TestPopulateThrowsOnDuplicatedKey() {
        parser.Parse("hello: world");
        level.Metadata["hello"] = "x";

        Assert.Throws<ParsingException>(() => parser.Populate(level));
    }
}
