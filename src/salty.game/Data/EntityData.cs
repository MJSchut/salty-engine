using System.Collections.Generic;
using MonoGame.Extended.Serialization;

namespace salty.game.Data
{
    
    public class EntityReader : JsonContentTypeReader<EntityData> { }
        
    public class EntityData
    {
        public TextureData TextureData;
        public HitBoxData HitboxData;
        public List<Cycle> Cycles;
    }

    public class TextureData
    {
        public string Texture;
        public int Width;
        public int Height;
    }
    
    public class HitBoxData
    {
        public int Width;
        public int Height;
        public int? XOffset;
        public int? YOffset;
    }

    public class Cycle
    {
        public string Name;
        public List<int> Frames;
        public bool IsLooping;
        public float FrameDuration;
    }
}