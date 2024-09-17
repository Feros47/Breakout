namespace BreakoutTests.Entities.Blocks.Factories;
using Breakout.Entities.Blocks.Factories;



public class UnbreakableFactoryTests {

    private UnbreakableFactory factory;

    [SetUp]
    public void Setup() {
        factory = new UnbreakableFactory();
    }

    [Test]
    public void TestReturnType() {
        Assert.True(factory is IBlockFactory && factory is UnbreakableFactory);
    }
}