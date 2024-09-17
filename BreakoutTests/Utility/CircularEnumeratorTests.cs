namespace BreakoutTests.Utility;

using System.Collections.Generic;
using Breakout.Utility;
using DIKUArcade.Math;
using static Breakout.Utility.MenuContainer;

[TestFixture]
public class CircularEnumeratorTests {
    CircularListEnumerator enumerator;

    [SetUp]
    public void SetUp() {
        var c = new MenuContainer(new List<string>() { "test1", "test2", "test3" }, new Vec2F(0.0f, 0.0f), new Vec2F(0.0f, 0.0f));
        enumerator = c.GetConcreteEnumerator();
    }

    [Test]
    public void Current() {
        Assert.That(enumerator.Current == "test1", Is.True);
    }

    [Test]
    public void MoveUp() {
        enumerator.MoveUp();
        //postcond.
        Assert.That(enumerator.Current, Is.EqualTo("test3"));
    }

    [Test]
    public void MoveUpLoops() {
        enumerator.MoveUp();
        enumerator.MoveUp();
        enumerator.MoveUp();
        //postcond.
        Assert.That(enumerator.Current, Is.EqualTo("test1"));
    }

    [Test]
    public void MoveDown() {
        enumerator.MoveUp();
        enumerator.MoveDown();
        //postcond.
        Assert.That(enumerator.Current, Is.EqualTo("test1"));
    }

    [Test]
    public void MoveDownLoops() {
        Assert.That(enumerator.Current, Is.EqualTo("test1"));
        enumerator.MoveDown();
        enumerator.MoveDown();
        enumerator.MoveDown();
        //postcond.
        Assert.That(enumerator.Current, Is.EqualTo("test1"));
    }

}
