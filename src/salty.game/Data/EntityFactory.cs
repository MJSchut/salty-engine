using System.Linq;
using DefaultEcs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using salty.core.Components;
using salty.core.Components.AI;
using salty.core.Components.EntityComponent;
using salty.core.Components.Interactables;
using salty.core.Components.Movement;
using salty.core.Util;

namespace salty.game.Data
{
    public static class EntityFactory
    {
        public static void CreateBedRoll(World world, ContentManager content, Vector2 position)
        {
            var bedRoll = world.CreateEntity();
            bedRoll.Set(new Transform2());
            bedRoll.Set(new SetPositionComponent(position.X, position.Y));
            var sprite = new Sprite(content.Load<Texture2D>("sprites/bedroll"));

            bedRoll.Set(sprite);
            bedRoll.Set<CursorTargetComponent>(new BedInteractableComponent(world));
            bedRoll.Set(new CollisionComponent(position.X, position.Y, 16, 32));
        }

        public static void CreatePlayer(World world, GraphicsDevice device, Vector2 position)
        {
            var player = world.CreateEntity();
            player.Set(new Transform2());
            player.Set(new PlayerComponent());
            player.Set(new ActorComponent(40f));
            player.Set(new StateComponent());

            var (x, y) = position;
            player.Set(new SetPositionComponent(x, y));
            player.Set(new CollisionComponent(x, y, 16, 16, -8, 0));

            var texture = Texture2DUtils.CreateColoredTexture(device, 16, 32, Color.Firebrick);
            player.Set(new Sprite(texture));
            player.Set(new PlayerWalletComponent());

            var cursorTexture = Texture2DUtils.CreateColoredTexture(device, 16, 16, Color.Gray);

            var cursor = world.CreateEntity();
            cursor.Set(new Transform2());
            cursor.Set(new Sprite(cursorTexture));
            cursor.Set(new SetPositionComponent(x, y));
            cursor.Set(new FollowComponent(player.Get<Transform2>()));
            cursor.Set(new CursorTriggerComponent());
            cursor.Set(new CollisionComponent(x, y, 16, 16, -8, -24, false));
            cursor.Set(new RestrictToGridComponent());
        }

        public static bool CreateAnimal(World world, ContentManager content, EntityData entityData, Vector2 position)
        {
            var animal = world.CreateEntity();
            animal.Set(new Transform2());
            animal.Set(new ActorComponent(25f));
            animal.Set<AiComponent>(new RandomWanderAiComponent());
            animal.Set(new StateComponent());

            var (x, y) = position;
            animal.Set(new SetPositionComponent(x, y));
            animal.Set(new CollisionComponent(x, y, entityData.HitboxData.Width, entityData.HitboxData.Height,
                entityData.HitboxData.XOffset, entityData.HitboxData.YOffset));

            var animalAtlas = TextureAtlas.Create("animalAtlas",
                content.Load<Texture2D>(entityData.TextureData.Texture),
                entityData.TextureData.Width,
                entityData.TextureData.Height);
            var spriteSheet = new SpriteSheet {TextureAtlas = animalAtlas};

            foreach (var cycle in entityData.Cycles)
                spriteSheet.Cycles.Add(cycle.Name,
                    AnimationCycleUtils.CreateAnimationCycle(cycle.Frames.ToArray(), cycle.IsLooping,
                        cycle.FrameDuration));

            var sprite = new AnimatedSprite(spriteSheet, "WalkingLeft");
            animal.Set(sprite);
            return true;
        }

        public static bool CreatePlant(World world, Plant plant, Vector2 position, SpriteSheet spriteSheet)
        {
            var newPlant = world.CreateEntity();
            newPlant.Set(new Transform2());
            newPlant.Set(new SetPositionComponent(position.X, position.Y));
            newPlant.Set(new RestrictToGridComponent());

            var plantComponent = new PlantComponent
            {
                NumberOfStages = plant.StageSprites.Distinct().Count(),
                DaysToMature = plant.StageSprites.Count,
                CurrentStage = 0
            };
            newPlant.Set(plantComponent);

            if (spriteSheet.Cycles.Count == 0)
                for (var i = 0; i < plant.StageSprites.Count; i++)
                {
                    var cycle = plant.StageSprites[i];
                    spriteSheet.Cycles.Add(i.ToString(),
                        AnimationCycleUtils.CreateAnimationCycle(new[] {cycle}, false, 1));
                }

            var sprite = new AnimatedSprite(spriteSheet, plantComponent.CurrentStage.ToString());
            newPlant.Set(sprite);
            newPlant.Set<CursorTargetComponent>(new PlantInteractableComponent(world, newPlant));
            newPlant.Set(new CollisionComponent(320, 240, 12, 12, -6, 6, false));

            newPlant.Set(new SellableComponent(plant.Value));
            return true;
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