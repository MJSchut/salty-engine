using System.Collections.Generic;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Serialization;
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

        public static void CreateAnimal(World world, ContentManager content, EntityData entityData, Vector2 position)
        {
            var animal = world.CreateEntity();
            animal.Set(new Transform2());
            animal.Set(new ActorComponent(25f));
            
            var (x, y) = position;
            animal.Set(new SetPositionComponent(x, y));
            animal.Set(new CollisionComponent(x, y, entityData.hitboxData.width, entityData.hitboxData.height, entityData.hitboxData.xOffset, entityData.hitboxData.yOffset));
            
            var chickenAtlas = TextureAtlas.Create("chickenAtlas", 
                content.Load<Texture2D>(entityData.textureData.texture), 
                entityData.textureData.width, 
                entityData.textureData.height);
            var spriteSheet = new SpriteSheet {TextureAtlas = chickenAtlas};

            foreach (var cycle in entityData.cycles)
                AddAnimationCycle(spriteSheet, cycle.name, cycle.frames.ToArray(), cycle.isLooping, cycle.frameDuration);

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
    
    public class EntityReader : JsonContentTypeReader<EntityData> { }
        
    public class EntityData
    {
        public TextureData textureData;
        public HitBoxData hitboxData;
        public List<Cycle> cycles;
    }

    public class TextureData
    {
        public string texture;
        public int width;
        public int height;
    }
    
    public class HitBoxData
    {
        public int width;
        public int height;
        public int? xOffset;
        public int? yOffset;
    }

    public class Cycle
    {
        public string name;
        public List<int> frames;
        public bool isLooping;
        public float frameDuration;
    }
}