namespace BreakoutTests.Entities.Blocks.Factories;
using Breakout.Entities.Blocks.Factories;



public class HardenedFactoryTests {

    private HardenedFactory factory;

    [SetUp]
    public void Setup() {
        factory = new HardenedFactory();
    }

    [Test]
    public void TestReturnType() {
        Assert.True(factory is IBlockFactory && factory is HardenedFactory);
    }
}