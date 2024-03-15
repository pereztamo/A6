using System.IO.Compression;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Player : IGameEventProcessor {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    public const float MOVEMENT_SPEED = 0.01f;
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        shape.Move();
        shape.Position.X = shape.Position.X < 0.0f ? 0.0f : shape.Position.X;
        shape.Position.X = shape.Position.X > 1.0f - shape.Extent.X ? 1.0f - shape.Extent.X : shape.Position.X;
    }

    private void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
    }

    public Vec2F GetPosition() {
        //remove .003 from x since the player sprite does not fill out the entire shape
        float newX = entity.Shape.Position.X + ((entity.Shape.Extent.X/2.0f) - 0.003f) ;
        float newY = entity.Shape.Position.Y - (entity.Shape.Extent.Y/2.0f);
        return new Vec2F(newX, newY);
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.MovementEvent) {
            switch (gameEvent.Message) {
                case "MoveLeft":
                    SetMoveLeft(true);
                break;
                case "MoveRight":
                    SetMoveRight(true);
                break;
                case "StopMoveLeft":
                    SetMoveLeft(false);
                break;
                case "StopMoveRight":
                    SetMoveRight(false);
                break;            
            }
        }
    }
}
