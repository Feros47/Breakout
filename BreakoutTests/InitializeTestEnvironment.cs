namespace BreakoutTests;
using System.Linq;
using Breakout;
using Breakout.State;
using DIKUArcade.GUI;
using NUnit.Framework;

[SetUpFixture]
public class InitializeTestEnv {
    public GameContext? gameContext;

    [OneTimeSetUp]
    public void InitializeTestEnvironment() {
        gameContext = GameContext.BuildContext<MainMenu>(new WindowArgs() { Title = "Breakout v0.1" });

        BreakoutBus
            .GetBus()
            .InitializeEventBus(Program.GameEventTypes.ToList());
    }
}