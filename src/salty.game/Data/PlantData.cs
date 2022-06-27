using System.Collections.Generic;
using MonoGame.Extended.Serialization;

namespace salty.game.Data
{
    public class PlantReader : JsonContentTypeReader<PlantData> { }

    public class PlantData
    {
        public List<Plant> Plants = new();
    }

    public class Plant
    {
        public string Name;
    }
}