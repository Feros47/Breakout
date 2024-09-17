namespace BreakoutTests.Stubs;

using System;
using Breakout.Entities.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

/// <summary>
/// Empty implementation of BaseBlock used to test its virtual methods.
/// </summary>
public class BaseBlockStub : BaseBlock {
    private int health = 10;
    private int _value = 10;
    public override int Value {
        get => _value;
        set {
            _value = value;
        }
    }
    public override int Health {

        get => health;
        set {
            health = value;
        }
    }
    private Shape shape = new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(0.0f, 0.0f));
    public override Shape Shape {
        get => shape;
        set => shape = value;
    }
    public override IBaseImage Image {
        get => throw new StubException("IMAGE");
        set => throw new StubException("IMAGE");
    }
    public override DynamicShape GetShape() => throw new NotImplementedException();
    public override void UnsubscribeFromEventBus() {
        // Intentional No-op
    }

    public override void HandleCollision() => throw new StubException("HANDLECOLLISION");
    public override void ProcessEvent(GameEvent gameEvent) => throw new StubException();

    // Used to test ShouldIgnore
    public bool Deleted {
        get; set;
    } = false;
    public override bool IsDeleted() => Deleted;
    public override void Render() => throw new StubException("RENDER_BASEBLOCK");
    public override void Update() => throw new NotImplementedException();
    public override void DeleteBlock() {
        Deleted = true;
    }
}
