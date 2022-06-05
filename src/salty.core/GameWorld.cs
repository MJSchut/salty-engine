using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using salty.core.Systems;

namespace salty.core
{
    public class GameWorld
    {
        private World _world;

        public GameWorld(GraphicsDevice device)
        {
            var worldBuilder = new WorldBuilder();
            _world = worldBuilder
                .AddSystem(new RenderSystem(device))
                .Build();
        }
        
        public void Update(GameTime gameTime){
            _world.Update(gameTime);
        }
        
        public void Draw(GameTime gameTime){
            _world.Draw(gameTime);
        }
    }
}