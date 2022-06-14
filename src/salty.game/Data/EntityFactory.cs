using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using salty.core.Components;
using salty.core.Util;

namespace salty.game.Data
{
    public static class EntityFactory
    {
        public static void CreatePlayer(World world, GraphicsDevice device)
        {
            var player = world.CreateEntity();
            player.Attach(new Transform2());
            player.Attach(new PlayerComponent());
            player.Attach(new ActorComponent(40f));
            var texture = Texture2DUtils.CreateColoredTexture(device, 16, 32, Color.Firebrick);
            player.Attach(new Sprite(texture));
        }

        public static void CreateTileMap(World world, ContentManager content)
        {
            var tileMap = world.CreateEntity();
            tileMap.Attach(new Transform2());
            tileMap.Attach(content.Load<TiledMap>("tilemaps/example_map"));
        }

        public static void CreateOrthographicCamera(World world, OrthographicCamera camera)
        {
            var cameraEntity = world.CreateEntity();
            cameraEntity.Attach(camera);
        }
    }
}