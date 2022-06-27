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
using salty.core.Components.AI;
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
            player.Set(new StateComponent());
            
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
            animal.Set<AiComponent>(new RandomWanderAiComponent());
            animal.Set(new StateComponent());

            var (x, y) = position;
            animal.Set(new SetPositionComponent(x, y));
            animal.Set(new CollisionComponent(x, y, entityData.HitboxData.Width, entityData.HitboxData.Height, entityData.HitboxData.XOffset, entityData.HitboxData.YOffset));
            
            var animalAtlas = TextureAtlas.Create("animalAtlas", 
                content.Load<Texture2D>(entityData.TextureData.Texture), 
                entityData.TextureData.Width, 
                entityData.TextureData.Height);
            var spriteSheet = new SpriteSheet {TextureAtlas = animalAtlas};

            foreach (var cycle in entityData.Cycles)
                spriteSheet.Cycles.Add(cycle.Name, AnimationCycleUtils.CreateAnimationCycle(cycle.Frames.ToArray(), cycle.IsLooping, cycle.FrameDuration));

            var sprite = new AnimatedSprite(spriteSheet, "WalkingLeft");
            animal.Set(sprite);
        }

        public static void CreatePlant(World world, Plant plant, SpriteSheet spriteSheet)
        {
            var newPlant = world.CreateEntity();
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