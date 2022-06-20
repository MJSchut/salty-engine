using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using salty.core.Components;
using salty.core.Util;

namespace salty.game.Data
{
    public static class EntityFactory
    {
        public static void CreatePlayer(World world, GraphicsDevice device, Vector2 position)
        {
            var player = world.CreateEntity();
            player.Set(new Transform2());
            player.Set(new PlayerComponent());
            player.Set(new ActorComponent(40f));
            var (x, y) = position;
            player.Set(new SetPositionComponent(x, y));
            player.Set(new CollisionComponent(x, y, 16, 16));

            var texture = Texture2DUtils.CreateColoredTexture(device, 16, 32, Color.Firebrick);
            player.Set(new Sprite(texture));
        }

        public static void CreateActor(World world, GraphicsDevice device, Vector2 position)
        {
            var player = world.CreateEntity();
            player.Set(new Transform2());
            player.Set(new ActorComponent(30f));
            var (x, y) = position;
            player.Set(new SetPositionComponent(x, y));
            player.Set(new CollisionComponent(x, y, 16, 16));

            var texture = Texture2DUtils.CreateColoredTexture(device, 16, 32, Color.Firebrick);
            player.Set(new Sprite(texture));
        }

        public static TiledMap CreateTileMap(World world, ContentManager content)
        {
            var tileMap = world.CreateEntity();
            tileMap.Set(new Transform2());
            tileMap.Set(content.Load<TiledMap>("tilemaps/example_map"));
            return tileMap.Get<TiledMap>();
        }
    }
}