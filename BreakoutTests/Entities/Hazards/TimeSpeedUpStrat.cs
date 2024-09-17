namespace BreakoutTests.Entities.Hazards;

using Breakout.Entities.DropItem.CreationStrategies.HazardStrategies;
using DIKUArcade.Math;

public class TimeSpeedUpItemTests : BreakoutTestBase {

    private TimerSpeedUpStrategy strategy;
    [SetUp]
    public void SetUp() {
        strategy = new();
    }

    [Test]
    public void CreateItem() {
        var item = strategy.CreateItem(new Vec2F(0f, 0f));
        Assert.That(item.PowerUpCreationStrategy, Is.TypeOf<TimerSpeedUpStrategy>());
    }
}