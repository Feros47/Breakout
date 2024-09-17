namespace BreakoutTests.Entities.Blocks.Factories;
using Breakout.Entities.Blocks.Factories;



public class PowerUpFactoryTests {

    private PowerUpFactory factory;

    [SetUp]
    public void Setup() {
        factory = new PowerUpFactory();
    }

    [Test]
    public void TestReturnType() {
        Assert.True(factory is IBlockFactory && factory is PowerUpFactory);
    }
}