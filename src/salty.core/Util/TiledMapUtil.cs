using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;

namespace salty.core.Util
{
    public static class TiledMapUtil
    {
        private const string SpawnPointLayer = "SpawnPoints";
        private const string PlayerSpawnPoint = "Player Spawn";
        private const string BedRollSpawnArea = "Bedroll Spawn";

        public static Vector2 GetPlayerPosition(TiledMap tileMap)
        {
            return tileMap.ObjectLayers
                .First(l => l.Name == SpawnPointLayer).Objects
                .First(e => e.Name == PlayerSpawnPoint).Position;
        }
        
        public static (Vector2 position, Size2 size) GetBedRollArea(TiledMap tileMap)
        {
            var area = tileMap.ObjectLayers
                .First(l => l.Name == SpawnPointLayer).Objects
                .First(e => e.Name == BedRollSpawnArea);
            
            return (area.Position, area.Size);
        }
    }
}