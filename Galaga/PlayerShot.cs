using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;
public class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.008f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.1f);

    public PlayerShot(Vec2F position, IBaseImage image) : base(new DynamicShape(position, extent, direction), image){

    }

    //According to the deliverable this method should exist, 
    //but was never mentioned in the actual assignment text so it just exists and is unused
    public void Render() {
        RenderEntity();
    }

    public void Move() {
        Shape.Move(direction);
    }
}
