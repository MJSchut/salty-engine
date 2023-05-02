using System.Collections.Generic;
using MonoGame.Extended.Serialization;

namespace salty.game.Data
{
    public class EntityReader : JsonContentTypeReader<EntityData>
    {
    }

    public class EntityData
    {
        public List<Cycle> Cycles;
        public HitBoxData HitboxData;
        public TextureData TextureData;
    }

    public class TextureData
    {
        public int Height;
        public string Texture;
        public int Width;
    }

    public class HitBoxData
    {
        public int Height;
        public int Width;
        public int? XOffset;
        public int? YOffset;
    }

    public class Cycle
    {
        public float FrameDuration;
        public List<int> Frames;
        public bool IsLooping;
        public string Name;
    }
}