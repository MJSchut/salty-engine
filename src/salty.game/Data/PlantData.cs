using System.Collections.Generic;
using MonoGame.Extended.Serialization;

namespace salty.game.Data
{
    public class PlantData
    {
        public List<Plant> Plants = new()
        {
            new Plant("Onion", "This is an onion", 120, new List<int> {0,1,1,2,2,2,3,3,3,4})
        };
    }

    public class Plant
    {
        public string Name;
        public string Description;
        public int Value;
        public readonly List<int> StageSprites;

        public Plant(string name, string description, int value, List<int> stageSprites)
        {
            Name = name;
            Description = description;
            Value = value;
            StageSprites = stageSprites;
        }
    }
}