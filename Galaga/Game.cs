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
using Galaga.Squadron;
using Galaga;

namespace Galaga;
public class Game : DIKUGame, IGameEventProcessor {
    private Player player;
    private GameEventBus eventBus;
    private List<ISquadron> currentSquadron;
    private int currentSquadronIndex;
    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "Player.png")));
        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.MovementEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.MovementEvent, player);

        currentSquadron = new List<ISquadron> {
            new TriangleSquadron(),
            new VerticalSquadron(),
            new HorizontalSquadron()
        };

        currentSquadronIndex = 0;
        ActivateSquadron(currentSquadronIndex);
        
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        
        enemyExplosions = new AnimationContainer(currentSquadron[currentSquadronIndex].MaxEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
        Path.Combine("Assets", "Images", "Explosion.png"));
        // TODO: Set key event handler (inherited window field of DIKUGame class)

    }

    private void ActivateSquadron(int index) {
        if (index >= 0 && index < currentSquadron.Count) {
            var squadron = currentSquadron[index];
            var enemyImages = ImageStride.CreateStrides(
                4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            var alternativeEnemyImages = ImageStride.CreateStrides(
                4, Path.Combine("Assets", "Images", "BlueMonster.png"));

            squadron.CreateEnemies(enemyImages, alternativeEnemyImages);

            // enemyExplosions.Clear();
            enemyExplosions = new AnimationContainer(squadron.MaxEnemies);
            }
        }

    public override void Render() {
        player.Render();
        currentSquadron[currentSquadronIndex].Enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
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
                eventBus.RegisterEvent(new GameEvent
                    {EventType = GameEventType.MovementEvent,
                    From = this,
                    To = player,
                    Message = "MoveLeft",
            });
            break;
            case KeyboardKey.Right:
            eventBus.RegisterEvent(new GameEvent
                    {EventType = GameEventType.MovementEvent,
                    From = this,
                    To = player,
                    Message = "MoveRight", 
            });
            break;
        }
    }

    private void KeyRelease(KeyboardKey key) {
        switch (key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
            break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent
                    {EventType = GameEventType.MovementEvent,
                    From = this,
                    To = player,
                    Message = "StopMoveLeft", 
            });
            break;
            case KeyboardKey.Right:
            eventBus.RegisterEvent(new GameEvent
                    {EventType = GameEventType.MovementEvent,
                    From = this,
                    To = player,
                    Message = "StopMoveRight",
            });
            break;
            case KeyboardKey.Space:
                eventBus.RegisterEvent(new GameEvent
                    {EventType = GameEventType.MovementEvent,
                    From = this,
                    To = this,
                    Message = "Shoot the ops", 
            });
            break;
        }
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
            if (shot.Shape.Position.X >= 1.0f) {
                shot.DeleteEntity();
            } else {
                currentSquadron[currentSquadronIndex].Enemies.Iterate(enemy => {
                    if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(), enemy.Shape).Collision) {
                        AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        enemyExplosions.AddAnimation(new StationaryShape(position, extent), 
            EXPLOSION_LENGTH_MS, 
            new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
        // TODO: add explosion to the AnimationContainer
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.MovementEvent) {
            switch (gameEvent.Message) {
                case "Shoot the ops":
                    playerShots.AddEntity(
                        new PlayerShot(player.GetPosition(), 
                        playerShotImage));
                break;
            }
        }
    }
}