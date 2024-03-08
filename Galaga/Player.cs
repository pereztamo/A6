using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class Player {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    public const float MOVEMENT_SPEED = 0.05f;
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

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
    }

    public void SetMoveRight(bool val) {
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

}
