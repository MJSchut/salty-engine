using System;
using System.Collections.Generic;
using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using salty.core.Components;
using salty.core.Components.Debugging;
using salty.core.Components.Input;
using salty.core.Systems.Ai;
using salty.core.Systems.Animations;
using salty.core.Systems.Camera;
using salty.core.Systems.Gameplay;
using salty.core.Systems.Input;
using salty.core.Systems.Interactables;
using salty.core.Systems.Movement;
using salty.core.Systems.Physics;
using salty.core.Systems.RenderSystems;
using salty.core.Util;
using salty.game.Data;

namespace salty.game
{
    public class GameWorld
    {
        private readonly World _world;
        private readonly SequentialSystem<float> _system;
        
        public OrthographicCamera Camera;

        public GameWorld(GraphicsDevice device, ContentManager content, OrthographicCamera camera)
        {
            Camera = camera;
            _world = new World();
            _world.Set(camera);
            _world.Set(new KeyboardComponent());
            _world.Set(new WorldTimeComponent());

            var font = content.Load<BitmapFont>("fonts/saltyfont");
            #if DEBUG
            _world.Set(new DebugRenderComponent(device));
            _world.Set(new DebugRenderUiComponent());
            _world.Set(new DebugControlComponent());
            #endif
            
            var tileMap = EntityFactory.CreateTileMap(_world, content);
            var playerPosition = TiledMapUtil.GetPlayerPosition(tileMap);
            var (bedRollPosition, _) = TiledMapUtil.GetBedRollArea(tileMap);
            
            EntityFactory.CreatePlayer(_world, device, playerPosition);
            EntityFactory.CreateBedRoll(_world, content, new Vector2(playerPosition.X - 24, playerPosition.Y));

            var entityActions = new EntityDataSystem(_world, content);
            
            var runner = new DefaultParallelRunner(Environment.ProcessorCount);
            _world.Set<IParallelRunner>(runner);
            
            var spriteBatch = new SpriteBatch(device);

            _system = new SequentialSystem<float>(
                // control systems
                new PlayerControlSystem(_world),
                new CameraControlSystem(_world),
                new AnimationControlSystem(_world, runner),
                new AiSystem(_world, runner),
                new PlayerMoneySystem(_world),
                new EntityDataSystem(_world, content),
                
                // movement systems
                new SetPositionSystem(_world),
                new FollowSystem(_world),
                new CursorInteractableSystem(_world),
                new RestrictToGridSystem(_world, runner),
                
                // world systems
                new CollisionSystem(_world, runner),
                new PlantSystem(_world),
                new WorldTimeSystem(_world),
                
                // input systems
                new KeyboardSystem(_world),
                
                // render systems
                new TilemapRenderSystem(_world, device, camera),
                new RenderSystem(_world, spriteBatch, camera)
                
                #if DEBUG
                ,new DebugRenderSystem(_world, spriteBatch, camera),
                new DebugRenderUiSystem(_world, spriteBatch, font),
                new DebugControlSystem(_world)
                #endif
            );
            _world.Optimize();
        }

        public void Update(GameTime gameTime)
        {
            _system.Update(gameTime.GetElapsedSeconds());
        }
    }
}