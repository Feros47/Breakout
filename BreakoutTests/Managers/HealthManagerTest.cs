namespace BreakoutTests.Managers;

using Breakout.Managers;
using NUnit.Framework;

public class BreakoutHealthManagerTests {
    private HealthManager healthManager;
    [SetUp]
    public void Setup() {
        healthManager = new HealthManager();
    }

    [Test]
    public void TestIncreaseHealth() {
        healthManager.IncreaseHealth();
        Assert.That(healthManager.Health, Is.EqualTo(4));
    }

    [Test]
    public void TestDecreaseHealth() {
        healthManager.DecreaseHealth();
        Assert.That(healthManager.Health, Is.EqualTo(2));
    }

    [Test]
    public void TestIncreaseDecreaseHealth() {
        healthManager.IncreaseHealth();
        healthManager.DecreaseHealth();
        Assert.That(healthManager.Health, Is.EqualTo(3));
    }

}