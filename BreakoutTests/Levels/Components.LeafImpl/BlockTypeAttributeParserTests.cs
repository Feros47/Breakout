namespace BreakoutTests.Levels.Components.LeafImpl;

using System;
using System.Linq;
using Breakout.Entities.Blocks.Factories;
using Breakout.Exceptions;
using Breakout.Levels;
using Breakout.Levels.Components.LeafImpl;

[TestFixture]
public class BlockTypeAttributeParserTests {
    [Test]
    public void TestInvalidArgsThrow() {
        Assert.Multiple(() => {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BlockTypeAttributeParser("he", "Hardened"));
            Assert.Throws<ArgumentOutOfRangeException>(() => new BlockTypeAttributeParser("h", "dhf"));
            Assert.Throws<ParsingException>(() => new BlockTypeAttributeParser("-", "Hardened"));
        });
    }

    [Test]
    public void TestCorrectFactoryTypes() {
        Assert.Multiple(() => {
            Assert.That(new BlockTypeAttributeParser("h", "Hardened").Factory, Is.TypeOf<HardenedFactory>());
            Assert.That(new BlockTypeAttributeParser("h", "Hazard").Factory, Is.TypeOf<HazardFactory>());
            Assert.That(new BlockTypeAttributeParser("h", "Moving").Factory, Is.TypeOf<MovingFactory>());
            Assert.That(new BlockTypeAttributeParser("h", "PowerUp").Factory, Is.TypeOf<PowerUpFactory>());
            Assert.That(new BlockTypeAttributeParser("h", "Unbreakable").Factory, Is.TypeOf<UnbreakableFactory>());
        });
    }

    [Test]
    public void TestPopulate() {
        var parser = new BlockTypeAttributeParser("x", "Hardened");
        var leveldata = new LevelData() {
            Metadata = new Metadata()
        };
        Assert.That(leveldata.Metadata.Factories['x'].Count, Is.EqualTo(0));
        parser.Populate(leveldata);

        Assert.Multiple(() => {
            Assert.That(leveldata.Metadata.Factories['x'].Count, Is.EqualTo(1));
            Assert.That(leveldata.Metadata.Factories['x'].FirstOrDefault(), Is.TypeOf<HardenedFactory>());
        });
    }
}
