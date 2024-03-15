using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using Galaga;
using DIKUArcade.Math;
using System.IO;

namespace Galaga.Squadron {
    public class TriangleSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; }
        public int MaxEnemies { get; } // Adjust to make a perfect triangle if needed

        public TriangleSquadron() {
            Enemies = new EntityContainer<Enemy>(MaxEnemies);
            MaxEnemies = 10;
        }

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
            int enemiesPlaced = 0;
            float spacing = 0.1f; 
            float midScreen = 0.45f;
            int enemiesNeeded = MaxEnemies;
            int rowSize = 1;

            while (enemiesNeeded > 0) {
                enemiesNeeded -= rowSize;
                rowSize++;
            }
            int numRows = rowSize - 1;

            for (int row = 0; row < numRows; row++) {
                float startX = midScreen - (row * spacing / 2);
                for (int i = 0; i <= row; i++) {
                    if (enemiesPlaced >= MaxEnemies) break;
                    Vec2F position = new Vec2F(startX + i * spacing, 0.8f - row * spacing);
                    Enemies.AddEntity(new Enemy(
                        new DynamicShape(position, new Vec2F(0.1f, 0.1f)), 
                        new ImageStride(80, enemyStride)));

                    enemiesPlaced++;
                }
            }
        }
    }

}