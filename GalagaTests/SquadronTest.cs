using NUnit.Framework;
using System.Collections.Generic;
using Galaga.Squadron;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;

namespace GalagaTests;
    [TestFixture]
    public class SquadronTests {
        private TriangleSquadron triangleSquadron;
        private VerticalSquadron verticalSquadron;
        private HorizontalSquadron horizontalSquadron;

        [SetUp]
        public void Setup() {
            triangleSquadron = new TriangleSquadron();
            verticalSquadron = new VerticalSquadron();
            horizontalSquadron = new HorizontalSquadron();
        }

        [Test]
        public void TestTriangleSquadron() {
            List<Image> enemyStride = ImageStride.CreateStrides(
                4, "../Galaga/Assets/Images/BlueMonster.png");
            List<Image> alternativeEnemyStride = ImageStride.CreateStrides(
                4, "../Galaga/Assets/Images/RedMonster.png");

            triangleSquadron.CreateEnemies(enemyStride, alternativeEnemyStride);
            Assert.AreEqual(10, triangleSquadron.Enemies.CountEntities());
        }

        [Test]
        public void TestVerticalSquadron() {
            List<Image> enemyStride = ImageStride.CreateStrides(
                4, "../Galaga/Assets/Images/BlueMonster.png");
            List<Image> alternativeEnemyStride = ImageStride.CreateStrides(
                4, "../Galaga/Assets/Images/RedMonster.png");

            verticalSquadron.CreateEnemies(enemyStride, alternativeEnemyStride);
            Assert.AreEqual(5, verticalSquadron.Enemies.CountEntities());
        }
            
        [Test]
        public void TestHorizontalSquadron() {
            List<Image> enemyStride = ImageStride.CreateStrides(
                4, "../Galaga/Assets/Images/BlueMonster.png");
            List<Image> alternativeEnemyStride = ImageStride.CreateStrides(
                4, "../Galaga/Assets/Images/RedMonster.png");

            horizontalSquadron.CreateEnemies(enemyStride, alternativeEnemyStride);
            Assert.AreEqual(8, horizontalSquadron.Enemies.CountEntities());

        }
    }