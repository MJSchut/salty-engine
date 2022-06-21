using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
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

        public static void CreateAnimal(World world, ContentManager content, Vector2 position)
        {
            var animal = world.CreateEntity();
            animal.Set(new Transform2());
            animal.Set(new ActorComponent(25f));
            
            var (x, y) = position;
            animal.Set(new SetPositionComponent(x, y));
            animal.Set(new CollisionComponent(x, y, 16, 16));

            var chickenAtlas = TextureAtlas.Create("chickenAtlas", content.Load<Texture2D>("sprites/chicken-sprites"), 16, 16);
            var spriteSheet = new SpriteSheet {TextureAtlas = chickenAtlas};
            
            AddAnimationCycle(spriteSheet, "IdleRight", new[] {0});
            AddAnimationCycle(spriteSheet, "WalkingRight", new[] {0,1,2,3}, true);
            AddAnimationCycle(spriteSheet, "IdleLeft", new[] {4});
            AddAnimationCycle(spriteSheet, "WalkingLeft", new[] {4,5,6,7}, true);
            
            var sprite = new AnimatedSprite(spriteSheet, "WalkingLeft");
            animal.Set(sprite);
        }

        public static TiledMap CreateTileMap(World world, ContentManager content)
        {
            var tileMap = world.CreateEntity();
            tileMap.Set(new Transform2());
            tileMap.Set(content.Load<TiledMap>("tilemaps/example_map"));
            return tileMap.Get<TiledMap>();
        }
        
        private static void AddAnimationCycle(SpriteSheet spriteSheet, string name, int[] frames, bool isLooping = true, float frameDuration = 0.1f)
        {
            var cycle = new SpriteSheetAnimationCycle();
            foreach (var f in frames)
            {
                cycle.Frames.Add(new SpriteSheetAnimationFrame(f, frameDuration));
            }

            cycle.IsLooping = isLooping;

            spriteSheet.Cycles.Add(name, cycle);
        }
    }
}