using System;
using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using salty.core.Components;
using salty.core.Components.Input;
using salty.core.Systems;
using salty.core.Systems.Input;
using salty.core.Systems.RenderSystems;
using salty.core.Util;
using salty.game.Data;
using SpriteSheetAnimationData = MonoGame.Extended.Sprites.SpriteSheetAnimationData;

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
            
            #if DEBUG
            _world.Set(new DebugRenderComponent(device));
            #endif
            
            var tileMap = EntityFactory.CreateTileMap(_world, content);
            
            var playerPosition = TiledMapUtil.GetPlayerPosition(tileMap);
            var chickenData = content.Load<EntityData>("data/chicken");
            
            EntityFactory.CreatePlayer(_world, device, playerPosition);

            for (var x = 20; x < 200; x+=20)
            {
                for (var y = 20; y < 200; y+=20)
                {
                    EntityFactory.CreateAnimal(_world, content, chickenData, new Vector2(playerPosition.X + x, playerPosition.Y + y));
                }
            }
            

            var runner = new DefaultParallelRunner(Environment.ProcessorCount);
            _world.Set<IParallelRunner>(runner);
            
            _system = new SequentialSystem<float>(
                // control systems
                new PlayerControlSystem(_world),
                new CameraControlSystem(_world),
                new AnimationControlSystem(_world, runner),
                new AiSystem(_world, runner),
                
                // consumption systems
                new SetPositionSystem(_world),
                
                // world systems
                new CollisionSystem(_world, runner),
                
                // input systems
                new KeyboardSystem(_world),
                
                // render systems
                new TilemapRenderSystem(_world, device, camera),
                new RenderSystem(_world, device, camera)
                #if DEBUG
                ,new DebugRenderSystem(_world, device, camera)
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