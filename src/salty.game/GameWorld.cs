using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
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
                .AddSystem(new PlayerControlSystem())
                .AddSystem(new RenderSystem(device))
                .Build();

            var player = _world.CreateEntity();
            player.Attach(new Transform2());
            player.Attach(new PlayerControlSystem());

            var texture = new Texture2D(device, 100, 100);
                
            var data = new Color[100 * 100];
            for(var pixel=0;pixel<data.Length;pixel++)
                data[pixel] = Color.White;

            //set the color
            texture.SetData(data);

            player.Attach(new Sprite(texture));
        }
        
        public void Update(GameTime gameTime){
            _world.Update(gameTime);
        }
        
        public void Draw(GameTime gameTime){
            _world.Draw(gameTime);
        }
    }
}