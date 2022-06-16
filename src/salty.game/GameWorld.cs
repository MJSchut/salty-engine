using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using salty.core.Systems;
using salty.game.Data;

namespace salty.game
{
    public class GameWorld
    {
        private readonly World _world;
        public OrthographicCamera camera;

        public GameWorld(GraphicsDevice device, ContentManager content, OrthographicCamera camera)
        {
            this.camera = camera;

            var worldBuilder = new WorldBuilder();
            _world = worldBuilder
                .AddSystem(new PlayerControlSystem())
                .AddSystem(new CameraControlSystem())
                
                .AddSystem(new SetPositionSystem())

                // rendering systems
                .AddSystem(new TilemapRenderSystem(device, camera))
                .AddSystem(new RenderSystem(device, camera))
                .Build();

            EntityFactory.CreatePlayer(_world, device);
            EntityFactory.CreateTileMap(_world, content);
            EntityFactory.CreateOrthographicCamera(_world, camera);
        }

        public void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _world.Draw(gameTime);
        }
    }
}