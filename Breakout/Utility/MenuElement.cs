#nullable disable
namespace Breakout.Utility;

using DIKUArcade.Graphics;

/// <summary>
/// "Wrapper" for the <see cref="Text"/> class, allowing the client to see the actual text displayed.
/// Note: This is a simple class, and setting Label wont affect the string displayed on screen!
/// </summary>
public class MenuElement {
    public string Label {
        get; set;
    }
    public Text Text {
        get; set;
    }
}