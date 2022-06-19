﻿using System;
using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using salty.core.Components;
using salty.core.Systems;
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
            
            #if DEBUG
            _world.Set(new DebugRenderComponent());
            #endif
            
            var tileMap = EntityFactory.CreateTileMap(_world, content);
            
            var playerPosition = TiledMapUtil.GetPlayerPosition(tileMap);
            EntityFactory.CreatePlayer(_world, device, playerPosition);

            var runner = new DefaultParallelRunner(Environment.ProcessorCount);
            _world.Set<IParallelRunner>(runner);

            _system = new SequentialSystem<float>(
                new PlayerControlSystem(_world),
                new CameraControlSystem(_world),
                new SetPositionSystem(_world),
                new CollisionSystem(_world, runner),
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