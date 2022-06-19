using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace salty.core.Util
{
    public static class TiledMapUtil
    {
        public const string SpawnPointLayer = "SpawnPoints";
        public const string PlayerSpawnPoint = "Player Spawn";

        public static Vector2 GetPlayerPosition(TiledMap tileMap)
        {
            return tileMap.ObjectLayers
                .First(l => l.Name == "SpawnPoints").Objects
                .First(e => e.Name == "Player Spawn").Position;
        }
    }
}