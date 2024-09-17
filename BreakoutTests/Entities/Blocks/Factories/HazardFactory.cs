namespace BreakoutTests.Entities.Blocks.Factories;
using Breakout.Entities.Blocks.Factories;



public class HazardFactoryTests {

    private HazardFactory factory;

    [SetUp]
    public void Setup() {
        factory = new HazardFactory();
    }

    [Test]
    public void TestReturnType() {
        Assert.True(factory is IBlockFactory && factory is HazardFactory);
    }
}