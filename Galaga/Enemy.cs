using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga;
public class Enemy : Entity{

    public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
    }

    //According to the deliverable this method should exist, 
    //but was never mentioned in the actual assignment text so it just exists and is unused
    public void Render() {
        RenderEntity();
    }
}
