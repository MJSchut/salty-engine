using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using salty.core.Components;
using salty.core.Systems;
using salty.core.Util;

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
                
                // rendering systems
                .AddSystem(new TilemapRenderSystem(device, camera))
                .AddSystem(new RenderSystem(device, camera))
                
                .Build();
            
            var player = _world.CreateEntity();
            player.Attach(new Transform2());
            player.Attach(new PlayerComponent());
            player.Attach(new ActorComponent(40f));
            var texture = Texture2DUtils.CreateColoredTexture(device, 16, 32, Color.Firebrick);
            player.Attach(new Sprite(texture));

            var tileMap = _world.CreateEntity();
            tileMap.Attach(new Transform2());
            tileMap.Attach(content.Load<TiledMap>("tilemaps/example_map"));
            
            var cameraEntity = _world.CreateEntity();
            cameraEntity.Attach(camera);

        }

        public void Update(GameTime gameTime){
            _world.Update(gameTime);
        }
        
        public void Draw(GameTime gameTime){
            _world.Draw(gameTime);
        }
    }
}