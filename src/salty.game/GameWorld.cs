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
            _world.Set(new DebugRenderComponent());
            #endif
            
            var tileMap = EntityFactory.CreateTileMap(_world, content);
            
            var playerPosition = TiledMapUtil.GetPlayerPosition(tileMap);
            
            EntityFactory.CreatePlayer(_world, device, playerPosition);
            EntityFactory.CreateAnimal(_world, content, new Vector2(playerPosition.X + 25, playerPosition.Y));

            var runner = new DefaultParallelRunner(Environment.ProcessorCount);
            _world.Set<IParallelRunner>(runner);
            
            _system = new SequentialSystem<float>(
                // control systems
                new PlayerControlSystem(_world),
                new CameraControlSystem(_world),
                
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