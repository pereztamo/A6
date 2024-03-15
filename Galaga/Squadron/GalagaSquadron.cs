using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using Galaga;
using System.IO;

namespace Galaga.Squadron {
    public interface ISquadron {
        EntityContainer<Enemy> Enemies {get;}
        int MaxEnemies {get;}

        void CreateEnemies (List<Image> enemyStride,
            List<Image> alternativeEnemyStride);
    }
}

