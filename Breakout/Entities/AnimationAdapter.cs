namespace Breakout.Entities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
/// <summary>
/// Adapter class between Animation and IBreakoutEntity
/// </summary>
public class AnimationAdapter : Animation, IBreakoutEntity {

    /// <summary>
    /// Constructor for AnimationAdapter
    /// </summary>
    /// <param name="duration">Duration of the animation</param>
    /// <param name="shape">Shape of the animation</param>
    /// <param name="stride">Stride of the animation</param>
    public AnimationAdapter(int duration, StationaryShape shape, ImageStride stride) {
        Duration = duration;
        Shape = shape;
        Stride = stride;
    }

    /// <summary>
    /// Check if entity should be deleted
    /// </summary>
    /// <returns>True of the animation is no longer active</returns>
    public bool IsDeleted() {
        return !IsActive();
    }

    /// <summary>
    /// Delete the entity
    /// </summary>
    public void Render() {
        RenderAnimation();
    }

    /// <summary>
    /// Update the entity
    /// </summary>
    public void Update() {
    }
}