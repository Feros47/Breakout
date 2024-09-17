namespace BreakoutTests.Stubs;

using Breakout.Levels;
using Breakout.Levels.Components;

public class ComponentStub : IComponent {
    public bool PopulateCalled {
        get; private set;
    } = false;
    public void Populate(LevelData data) {
        PopulateCalled = true;
    }
}
