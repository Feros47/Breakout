namespace Breakout.Levels.Components;
using System.Collections.Generic;

public interface IComposite : IComponent {
    List<IComponent> Children {
        get;
    }
    void Parse(string sectionData);
}
