namespace Breakout.Entities;
/// <summary>
/// IBreakoutEntity is a common interface for all entities in the Breakout game.
/// </summary>
public interface IBreakoutEntity {
    /// <summary>
    /// Render the entity.
    /// </summary>
    void Render();
    /// <summary>
    /// Update the entity by a single timestep.
    /// </summary>
    void Update();

    /// <summary>
    /// Detemine if the entity is deleted.
    /// </summary>
    /// <returns>True if the entity is to be deleted, false otherwise.</returns>
    bool IsDeleted();
}
