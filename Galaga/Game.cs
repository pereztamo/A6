using System.IO;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using System.Collections.Generic;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private GameEventBus eventBus;
    private EntityContainer<Enemy> enemies;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);

        List<Image> images = ImageStride.CreateStrides (
            4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8; 
        enemies = new EntityContainer<Enemy>(numEnemies); 
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
                new ImageStride(80, images)));
            }
        
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        
        // TODO: Set key event handler (inherited window field of DIKUGame class)

    }

    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
    }

    public override void Update() {
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }

    private void KeyPress(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
            case KeyboardKey.Space:
                PlayerShot playerShot = new PlayerShot(player.GetPosition(), playerShotImage);
                playerShots.AddEntity(playerShot);
                break;
        }
        // TODO: Close window if escape is pressed
        // TODO: switch on key string and set the player's move direction
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Left:
                player.SetMoveLeft(false);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(false);
                break;
        }
        // TODO: switch on key string and disable the player's move direction
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        switch (action) {
            case KeyboardAction.KeyRelease:
                KeyRelease(key);
                break;
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
            }

        // TODO: Switch on KeyBoardAction and call proper method
        }

    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Move();
            if (shot.Shape.Position.X > 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }

    public void ProcessEvent(GameEvent gameEvent) {
        // Leave this empty for now
    }
}
