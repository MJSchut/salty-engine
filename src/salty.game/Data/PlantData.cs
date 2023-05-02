using System.Collections.Generic;

namespace salty.game.Data
{
    public class PlantData
    {
        public List<Plant> Plants = new()
        {
            new Plant("Onion", 100, new List<int> {0, 1, 1, 2, 2, 2, 3, 3, 3, 4})
        };
    }

    public class Plant
    {
        public readonly List<int> StageSprites;
        public string Name;
        public int Value;

        public Plant(string name, int value, List<int> stageSprites)
        {
            Name = name;
            StageSprites = stageSprites;
            Value = value;
        }
    }
}