using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using salty.core.Components;
using salty.core.Systems;
using salty.core.Util;

namespace salty.game
{
    public class GameWorld
    {
        private readonly World _world;
        public OrthographicCamera camera;

        public GameWorld(GraphicsDevice device, OrthographicCamera camera)
        {
            var worldBuilder = new WorldBuilder();
            _world = worldBuilder
                .AddSystem(new PlayerControlSystem())
                .AddSystem(new RenderSystem(device, camera))
                .Build();
            
            var player = _world.CreateEntity();
            player.Attach(new Transform2());
            player.Attach(new PlayerComponent());
            var texture = Texture2DUtils.CreateColoredTexture(device, 100, 100, Color.Firebrick);
            player.Attach(new Sprite(texture));

            this.camera = camera;
        }

        public void Update(GameTime gameTime){
            _world.Update(gameTime);
        }
        
        public void Draw(GameTime gameTime){
            _world.Draw(gameTime);
        }
    }
}