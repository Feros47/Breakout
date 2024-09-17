namespace BreakoutTests.Entities.Blocks.Factories;
using Breakout.Entities.Blocks.Factories;



public class MovingFactoryTests {

    private MovingFactory factory;

    [SetUp]
    public void Setup() {
        factory = new MovingFactory();
    }

    [Test]
    public void TestReturnType() {
        Assert.True(factory is IBlockFactory && factory is MovingFactory);
    }
}