namespace GalagaTests;

using NUnit.Framework;
using System.IO;
using Galaga;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade;
using DIKUArcade.Events;


public class TestsPlayer{

    private Player player;

    private Vec2F startingPosition;

    [SetUp]
    public void Setup() {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new NoImage());
        startingPosition = player.GetPosition();
    }

    [Test]
    public void TestMoveRight() {
        player.ProcessEvent(
            new GameEvent {
                EventType = GameEventType.MovementEvent, 
                Message = "MoveRight"
            });

        player.Move();
        Vec2F newPosition = player.GetPosition();
        Assert.That(newPosition.X, Is.EqualTo(startingPosition.X + Player.MOVEMENT_SPEED).Within(1).Ulps);
    }

    [Test]
    public void TestMoveLeft() {
        player.ProcessEvent(
            new GameEvent {
                EventType = GameEventType.MovementEvent, 
                Message = "MoveLeft"
            });
        player.Move();
        Vec2F newPosition = player.GetPosition();
        Assert.That(newPosition.X, Is.EqualTo(startingPosition.X - Player.MOVEMENT_SPEED).Within(1).Ulps);
    }

    [Test]
    public void TestStay() {
        player.Move();
        Vec2F newPosition = player.GetPosition();
        Assert.That(newPosition.X, Is.EqualTo(startingPosition.X).Within(1).Ulps);
    }

    [Test]
    public void TestMoveEquilibrium() {
        player.ProcessEvent(
            new GameEvent {
                EventType = GameEventType.MovementEvent, 
                Message = "MoveRight"
            });
        player.ProcessEvent(
            new GameEvent {
                EventType = GameEventType.MovementEvent, 
                Message = "MoveLeft"
            });
        player.Move();
        Vec2F newPosition = player.GetPosition();
        Assert.That(newPosition.X, Is.EqualTo(startingPosition.X).Within(1).Ulps);
    }
}