using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using Galaga;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.Squadron {
    public class VerticalSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies {get;}
        public int MaxEnemies {get;}

        public VerticalSquadron() {
            Enemies = new EntityContainer<Enemy>();
            MaxEnemies = 5; 
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            float startXL = 0.8f;
            float startY = 0.9f;
            float spacing = 0.1f;
            float startX = 0.1f;

            for (int i = 0; i < MaxEnemies; i++) {
                float positionY = startY - (i * spacing);
                if (positionY > 0.1f && positionY < 0.9f) {
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(startXL, positionY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride)));

                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(new Vec2F(startX, positionY), new Vec2F(0.1f, 0.1f)),
                        new ImageStride(80, enemyStride)));
                }
            }
        }
    }
}