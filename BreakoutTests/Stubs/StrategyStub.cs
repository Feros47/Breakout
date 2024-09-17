namespace BreakoutTests.Stubs;

using Breakout.Entities.DropItem;
using Breakout.Entities.DropItem.CreationStrategies;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

public class StrategyStub : IDropItemCreationStrategy {
    public DropItem CreateItem(Vec2F pos) {
        throw new System.NotImplementedException();
    }

    public void CreateExpendable(Vec2F pos) {
        throw new System.NotImplementedException();
    }

    public IBaseImage PowerupIcon {
        get => throw new StubException("STRATEGY_ICON");
    }
}