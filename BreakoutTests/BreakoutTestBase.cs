namespace BreakoutTests;

using System.Linq;
using Breakout;

[TestFixture]
public class BreakoutTestBase {
    [SetUp]
    public void ClearEventBus() {
        BreakoutBus.ResetInstance(Program.GameEventTypes.ToList());
    }
}
