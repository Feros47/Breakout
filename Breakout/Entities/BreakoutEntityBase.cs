namespace Breakout.Entities;

using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

/// <summary>
/// Class adapter between IBreakoutEntity and Entity.
/// Used since entities apart from blocks can just inherit from Entity.
/// </summary>
public abstract class BreakoutEntityBase : Entity, IBreakoutEntity {

    /// <summary>
    /// Constructor for BreakoutEntityBase.
    /// </summary>
    /// <param name="shape">Shape of the entity.</param>
    /// <param name="image">Image of the entity.</param>
    /// <returns>BreakoutEntityBase</returns>
    public BreakoutEntityBase(Shape shape, IBaseImage image) : base(shape, image) {
    }
    ~BreakoutEntityBase() {
        if (this is IGameEventProcessor p) {
            Program
                .GameEventTypes
                .ToList()
                .ForEach(t => BreakoutBus.GetBus().Unsubscribe(t, p));
        }
    }

    public abstract void Render();
    public abstract void Update();
}
