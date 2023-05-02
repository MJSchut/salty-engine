using System;
using System.Collections.Generic;
using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace salty.game.Data
{
    public class EntityDataSystem : ISystem<float>
    {
        public readonly Dictionary<string, Func<Vector2, bool>> ActionList = new();

        public EntityDataSystem(World world, ContentManager content)
        {
            var chickenData = content.Load<EntityData>("data/chicken");
            
            var plantData = new PlantData();
            var plantSprites = content.Load<Texture2D>("sprites/plants");
            var plantAtlas = TextureAtlas.Create("plantAtlas", plantSprites, 16, 32);
            var plantSpriteSheet = new SpriteSheet {TextureAtlas = plantAtlas};

            ActionList.Add("onion",
                vec => EntityFactory.CreatePlant(world, plantData.Plants.First(), vec, plantSpriteSheet));
            ActionList.Add("chicken", vec => EntityFactory.CreateAnimal(world, content, chickenData, vec));
        }

        public void Dispose()
        {
        }

        public void Update(float state)
        {
        }

        public bool IsEnabled { get; set; }
    }
}