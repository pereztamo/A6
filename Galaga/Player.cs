using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga;
public class Player {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    public const float MOVEMENT_SPEED = 0.01f;
    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }
    // TODO: Add private fields

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }

    public void Render() {
        entity.RenderEntity();
        // TODO: render the player entity
    }

    public void Move() {
        shape.Position.X = shape.Position.X < 0.0f ? 0.0f : shape.Position.X;
        shape.Position.X = shape.Position.X > 1.0 - shape.Extent.X ? 1.0f - shape.Extent.X : shape.Position.X;
        shape.Move();
        // TODO: move the shape and guard against the window borders*/
    }

    public void SetMoveLeft(bool val) {
        if (val) {
            moveLeft -= MOVEMENT_SPEED;
        } else {
            moveLeft = 0.0f;
        }
        UpdateDirection();
        // TODO:set moveLeft appropriately and call UpdateDirection()
    }

    public void SetMoveRight(bool val) {
        if (val) {
            moveRight += MOVEMENT_SPEED;
        } else {
            moveRight = 0.0f;
        }
        UpdateDirection();
        // TODO:set moveRight appropriately and call UpdateDirection()
    }

}
