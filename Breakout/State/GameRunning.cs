#nullable disable
namespace Breakout.State;

using Breakout.Entities;
using DIKUArcade.Events;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Utilities;
using Entities.Blocks;
using Entities.Blocks.Decorators;
using Entities.Collidable;
using Entities.DropItem.PowerUps;
using Levels;
using Managers;
using Utility;

/// <summary>
/// The GameRunning class is responsible for managing the game state when the game is actively running.
/// It handles the loading of levels, managing game entities (such as the player, blocks, and balls),
/// processing game events, handling collisions, and determining game win/loss conditions.
/// </summary>
public class GameRunning : BreakoutState, IGameEventProcessor {
    private readonly int level;
    private List<IBreakoutEntity> entities;
    private readonly Player player;
    private HealthManager healthManagerInstance;
    private PointManager pointManagerInstance;
    private int healthInList;
    private int offSet;

    public GameRunning(int iLevel, HealthManager healthManager, PointManager pointManager) {
        BreakoutBus
            .ClearEvents();
        var playerImg = DIKUArcadeExtensions.LoadImageFromAssets("player.png");
        var ballImg = DIKUArcadeExtensions.LoadImageFromAssets("ball.png");
        healthManagerInstance = healthManager;
        pointManagerInstance = pointManager;
        player = new Player(playerImg);


        var path = Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", $"level{iLevel}.txt");
        level = iLevel;
        var levelData = LevelLoader.LoadFromConfiguration(path);

        entities = new List<IBreakoutEntity>();
        var blocks = levelData?.CreateBlocks() ?? new List<BaseBlock>();

        foreach (var block in blocks) {
            entities.Add(block);
        }
        entities.Add(new Ball(ballImg));
        entities.Add(player);
        entities.Add(pointManagerInstance);


        // For the powerup-related events
        BreakoutBus
            .GetBus()
            .Subscribe(GameEventType.GraphicsEvent, this);
        // For Timer events
        BreakoutBus
            .GetBus()
            .Subscribe(GameEventType.StatusEvent, this);

        if (levelData.Metadata.ContainsKey("Time") && int.TryParse(levelData.Metadata["Time"], out var t)) {
            entities.Add(new Timer(t));
        }
        //timer = new Timer(100);
    }
    ~GameRunning() {
        BreakoutBus
            .ClearEvents();
    }

    /// <summary>
    /// Get all entities in GameRunning instance
    /// </summary>
    /// <returns>List of IBreakoutEntity</returns>
    public List<IBreakoutEntity> GetEntities() {
        return entities;
    }

    #region BreakoutState
    /// <summary>
    /// No-op
    /// </summary>
    public override void ResetState() {
        // intentional no-op since GamePaused will call SwitchState on this object
    }
    /// <summary>
    /// Update the game state
    /// </summary>
    public override void UpdateState() {
        HandleCollisions();
        entities = IterateEntities(e => e.Update());
        LosePlayerHealth();
        UpdateHealthList();


        // If no blocks are left or all blocks are unbreakable, the game is won
        if (IsWon()) {
            // If a higher level exists, we continue, otherwise we go back to the main menu
            var path = Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", $"level{level + 1}.txt");
            if (!File.Exists(path)) {
                Context.SwitchState(new WinState(pointManagerInstance));
            } else {
                Context.SwitchState(new GameRunning(level + 1, healthManagerInstance, pointManagerInstance));
            }
        }
    }
    /// <summary>
    /// Render the state
    /// </summary>
    public override void RenderState() {
        entities = IterateEntities(e => e.Render());
    }
    /// <summary>
    /// Handle key events
    /// </summary>
    /// <param name="action"></param>
    /// <param name="key"></param>
    public override void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (key) {
            case KeyboardKey.A:
            case KeyboardKey.D:
            case KeyboardKey.Space:
                BreakoutBus
                    .GetBus()
                    .RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        To = player,
                        Message = action.ToString(),
                        IntArg1 = (int) key
                    });
                break;
            case KeyboardKey.Escape:
                Context.SwitchState(new GamePaused(this));
                break;
        }
    }
    #endregion

    #region IGameEventProcessor
    /// <summary>
    /// Process a game event that this state is subscribed to
    /// </summary>
    /// <param name="gameEvent"></param>
    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.GraphicsEvent) {
            switch (gameEvent.Message) {
                case "POWERUP_EXPEND_BALLSPLIT":
                    AddSplitBalls();
                    break;
                case "POWERUP_ITEM":
                case "POWERUP_EXPEND":
                case "ROCKET_EXPLOSION":
                case "HAZARD_ITEM":
                    entities.Add(gameEvent.ObjectArg1 as IBreakoutEntity);
                    break;
            }
        }

        if (gameEvent.EventType == GameEventType.StatusEvent &&
            gameEvent.Message == "TIMEOUT") {
            Context.SwitchState(new LoseState(pointManagerInstance));
        }
    }
    #endregion

    #region GameRunning private implementation
    private void HandleCollisions() {
        var collidables = entities.OfType<ICollidable>();
        foreach (var actor in collidables.Where(ic => !ic.ShouldIgnore())) {
            foreach (var subject in collidables.Where(ic => !ic.ShouldIgnore())) {
                var collisionData = CollisionDetection.Aabb(actor.GetShape(), subject.GetShape().AsStationaryShape());
                if (collisionData.Collision) {
                    var visitor = actor.MakeVisitor();
                    subject.AcceptCollision(visitor, collisionData);
                    actor.AcceptCollision(subject.MakeVisitor(), collisionData);
                }
                if (actor.ShouldIgnore()) {
                    break;
                }
            }
        }
    }

    private void AddSplitBalls() {
        Ball newBall1 = new Ball(DIKUArcadeExtensions.LoadImageFromAssets("ball.png"));
        Ball newBall2 = new Ball(DIKUArcadeExtensions.LoadImageFromAssets("ball.png"));
        Ball currentBall = entities.OfType<Ball>().FirstOrDefault();
        if (currentBall != null) {
            newBall1.ChangeDirection(new CollisionData { CollisionDir = CollisionDirection.CollisionDirLeft });
            newBall1.GetShape().Position.X = currentBall.Position.X + 0.02f;
            newBall1.GetShape().Position.Y = currentBall.Position.Y;
            newBall2.ChangeDirection(new CollisionData { CollisionDir = CollisionDirection.CollisionDirRight });
            newBall2.GetShape().Position.X = currentBall.Position.X - 0.02f;
            newBall2.GetShape().Position.Y = currentBall.Position.Y;
            entities.Add(newBall1);
            entities.Add(newBall2);
        }
    }
    /// <summary>
    /// Determine if a game is won or not by two rules:
    /// (1) if there are no <see cref="Block"/> entities left
    /// (2) if the only <see cref="Block"/> entities left are of the <see cref="UnbreakableBlock"/> type.
    /// </summary>
    /// <returns>true if the level is won, false otherwise.</returns>
    private bool IsWon() {
        var blocks = entities.OfType<BaseBlock>();
        return !blocks.Any() ||
            blocks.Where(b => b.IsBlockType<UnbreakableBlockDecorator>()).Count() == blocks.Count();
    }

    /// <summary>
    /// Determine if a game is lost. A game is lost whenever there are no balls left on the screen.
    /// </summary>
    /// <returns>True if the game is lost, false otherwise.</returns>
    private void LosePlayerHealth() {
        if (entities.OfType<Ball>().Count() == 0 && entities.OfType<PowerUpExpendHardBall>().Count() == 0) {
            healthManagerInstance.DecreaseHealth();
            entities.Add(new Ball(DIKUArcadeExtensions.LoadImageFromAssets("ball.png")));
        }
        if (healthManagerInstance.Health == 0) {
            Context.SwitchState(new LoseState(pointManagerInstance));
        }
    }

    private List<IBreakoutEntity> IterateEntities(Action<IBreakoutEntity> action) {
        var newList = new List<IBreakoutEntity>(entities.Count);

        foreach (var entity in entities) {
            action(entity);
            if (!entity.IsDeleted()) {
                newList.Add(entity);
            } else if (entity is IGameEventProcessor ep) {
                UnsubFromEverything(ep);
            }
        }
        return newList;
    }

    /// <summary> 
    /// Unsubscribe for every type of GameEvents
    /// </summary>
    public void UnsubFromEverything(IGameEventProcessor processor) {
        foreach (var t in Program.GameEventTypes) {
            BreakoutBus
                .GetBus()
                .Unsubscribe(t, processor);
        }
    }

    private void UpdateHealthList() {
        if (healthInList > healthManagerInstance.Health) {
            entities.OfType<HealthUI>().Last().DeleteEntity();
            healthInList--;
            offSet--;
        } else if (healthInList < healthManagerInstance.Health) {
            entities.Add(new HealthUI(new Vec2F(offSet * 0.025f, 0.020f)));
            healthInList++;
            offSet++;
        }
    }


    #endregion
}
