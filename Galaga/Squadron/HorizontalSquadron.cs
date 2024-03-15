using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using Galaga;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.Squadron {
public class HorizontalSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}

        public HorizontalSquadron() {
            Enemies = new EntityContainer<Enemy>();
            MaxEnemies = 8; 
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            float spacing = 0.1f;
            for (int i = 0; i < MaxEnemies; i++) {
                if ((i * spacing + 0.1f) > 0.1 && (i * spacing +0.1f) < 0.9) {
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(i * spacing + 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride)));
                }
            }
        }
    }
}