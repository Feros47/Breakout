namespace BreakoutTests.Levels.Components.CompositeImpl;

using System;
using System.Collections.Generic;
using System.Linq;
using Breakout.Exceptions;
using Breakout.Levels;
using Breakout.Levels.Components.CompositeImpl;
using BreakoutTests.Stubs;

[TestFixture]
public class LevelDataParserTests {
    private LevelDataParser parser;
    private LevelData level;
    [SetUp]
    public void SetUp() {
        parser = new LevelDataParser();
        level = new LevelData();
    }

    [TestCase("")]
    [TestCase("\n")]
    [TestCase(" ")]
    public void TestInvalidString(string input) {
        Assert.Throws<ParsingException>(() => parser.Parse(input));
    }

    [TestCase("Map:\n-\nMap/", typeof(AsciiMapParser))]
    [TestCase("Meta:\nMeta/", typeof(MetadataParser))]
    [TestCase("Legend:\nLegend/", typeof(LegendParser))]
    public void TestSectionType(string input, Type childType) {
        parser.Parse(input);
        Assert.Multiple(() => {
            Assert.That(parser.Children.Count, Is.EqualTo(1));
            Assert.That(parser.Children.First(), Is.TypeOf(childType));
        });
    }

    [Test]
    public void TestUnknownSectionType() {
        var input = "SomeUnknownSection:\nSomeUnknownSection/";
        Assert.Throws<ParsingException>(() => parser.Parse(input));
    }

    [TestCase(0)]
    [TestCase(1)]
    public void TestPopulate(int childCount) {
        for (var i = 0; i < childCount; i++) {
            parser.Children.Add(new ComponentStub());
        }
        var stubs = parser.Children.Cast<ComponentStub>();
        parser.Populate(level);
        Assert.That(stubs.All(s => s.PopulateCalled), Is.True);
    }

    [Test]
    public void TestReadSectionImmediateFollowup() {
        var strings = new List<string> {
            "Section/"
        };
        var enumm = LevelDataParser.ReadSection(
            strings.GetEnumerator(),
            "Section",
            out var sectionData);

        Assert.Multiple(() => {
            // We should be at the last element now
            Assert.That(enumm.Current, Is.EqualTo("Section/"));
            Assert.That(sectionData, Is.EqualTo(string.Empty));
        });
    }
    [Test]
    public void TestReadSectionToEof() {
        var strings = new List<string> {
            "data1", "data2"
        };
        var enumm = LevelDataParser.ReadSection(
            strings.GetEnumerator(),
            "Section",
            out var sectionData);

        Assert.Multiple(() => {
            // We should be at the last element now
            Assert.That(enumm.Current, Is.Null);
            Assert.That(sectionData, Is.EqualTo("data1\ndata2\n"));
        });
    }
    [Test]
    public void TestReadSectionToEndTag() {
        var strings = new List<string> {
            "data1",
            "Section/",
            "data2"
        };
        var enumm = LevelDataParser.ReadSection(
            strings.GetEnumerator(),
            "Section",
            out var sectionData);

        Assert.Multiple(() => {
            Assert.That(enumm.Current, Is.EqualTo("Section/"));
            Assert.That(sectionData, Is.EqualTo("data1\n"));
        });
    }
}
