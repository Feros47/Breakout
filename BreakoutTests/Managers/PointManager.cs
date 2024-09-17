namespace BreakoutTests.Managers;

using Breakout.Managers;
using NUnit.Framework;

public class BreakoutPointManagerTests {
    private PointManager pointManager;
    [SetUp]
    public void Setup() {
        pointManager = new PointManager();
    }

    [Test]
    public void TestIncreasePoints() {
        pointManager.AddPoints(10);
        Assert.That(pointManager.points, Is.EqualTo(10));
    }


}