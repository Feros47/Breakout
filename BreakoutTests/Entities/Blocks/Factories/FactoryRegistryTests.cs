namespace BreakoutTests.Entities.Blocks.Factories;

using Breakout.Entities.Blocks.Factories;

[TestFixture]
public class FactoryRegistryTests {
    private FactoryRegistry registry;

    [SetUp]
    public void SetUp() {
        registry = new FactoryRegistry();
    }

    [Test]
    public void TestIsBlockTypeDesignator() {
        Assert.Multiple(() => {
            Assert.That(registry.IsBlockTypeDesignator("Hardened"), Is.True);
            Assert.That(registry.IsBlockTypeDesignator("Unbreakable"), Is.True);
            Assert.That(registry.IsBlockTypeDesignator("PowerUp"), Is.True);
            Assert.That(registry.IsBlockTypeDesignator("Moving"), Is.True);
            Assert.That(registry.IsBlockTypeDesignator("Hazard"), Is.True);
            Assert.That(registry.IsBlockTypeDesignator("test"), Is.False);
        });
    }

    [Test]
    public void TestHasHardened() {
        var hardened = registry["Hardened"];

        Assert.Multiple(() => {
            Assert.That(hardened, Is.Not.Null);
            Assert.That(hardened, Is.TypeOf<HardenedFactory>());
        });
    }

    [Test]
    public void TestHasUnbreakable() {
        var unbreakable = registry["Unbreakable"];

        Assert.Multiple(() => {
            Assert.That(unbreakable, Is.Not.Null);
            Assert.That(unbreakable, Is.TypeOf<UnbreakableFactory>());
        });
    }

    [Test]
    public void TestHasPowerUp() {
        var powerup = registry["PowerUp"];

        Assert.Multiple(() => {
            Assert.That(powerup, Is.Not.Null);
            Assert.That(powerup, Is.TypeOf<PowerUpFactory>());
        });
    }

    [Test]
    public void TestHasMoving() {
        var moving = registry["Moving"];

        Assert.Multiple(() => {
            Assert.That(moving, Is.Not.Null);
            Assert.That(moving, Is.TypeOf<MovingFactory>());
        });
    }

    [Test]
    public void TestHasHazard() {
        var hazard = registry["Hazard"];

        Assert.Multiple(() => {
            Assert.That(hazard, Is.Not.Null);
            Assert.That(hazard, Is.TypeOf<HazardFactory>());
        });
    }
}
