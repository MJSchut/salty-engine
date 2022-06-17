using System;
using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using salty.core.Systems;
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
            this.Camera = camera;
            _world = new World();
            _world.Set(camera);
            
            var tileMap = EntityFactory.CreateTileMap(_world, content);
            var playerPosition = tileMap.ObjectLayers
                .First(l => l.Name == "SpawnPoints").Objects
                .First(e => e.Name == "Player Spawn").Position;
            EntityFactory.CreatePlayer(_world, device, playerPosition);

            var runner = new DefaultParallelRunner(Environment.ProcessorCount);
            _world.Set<IParallelRunner>(runner);

            _system = new SequentialSystem<float>(
                new PlayerControlSystem(_world),
                new CameraControlSystem(_world),
                new SetPositionSystem(_world),
                new TilemapRenderSystem(_world, device, camera),
                new RenderSystem(_world, device, camera));
            
            _world.Optimize();
        }

        public void Update(GameTime gameTime)
        {
            _system.Update(gameTime.GetElapsedSeconds());
        }
    }
}