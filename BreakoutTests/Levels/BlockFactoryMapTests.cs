namespace BreakoutTests.Levels;

using System.Linq;
using Breakout.Entities.Blocks.Factories;
using Breakout.Levels;

[TestFixture]
public class BlockFactoryMapTests {
    private BlockFactoryMap map;
    [SetUp]
    public void SetUp() {
        map = new();
    }

    [Test]
    public void TestNonExistingKey() {
        var v = map['x'];

        Assert.Multiple(() => {
            Assert.That(v, Is.Not.Null);
            Assert.That(v.Count, Is.EqualTo(0));
        });
    }

    [Test]
    public void TestPrepends() {
        Assert.That(map['x'].Count, Is.EqualTo(0));
        map.Add('x', new HardenedFactory());
        Assert.Multiple(() => {
            Assert.That(map['x'].Count, Is.EqualTo(1));
            Assert.That(map['x'].First(), Is.TypeOf<HardenedFactory>());
        });
        map.Add('x', new MovingFactory());
        Assert.Multiple(() => {
            Assert.That(map['x'].Count, Is.EqualTo(2));
            Assert.That(map['x'].First(), Is.TypeOf<MovingFactory>());
        });
    }
}
