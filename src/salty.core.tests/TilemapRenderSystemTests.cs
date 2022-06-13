using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using salty.core.Systems;
using Xunit;

namespace salty.core.tests
{
    public class TilemapRenderSystemTests
    {
        [Fact]
        public void TileMapRendererCanBeInitialized()
        {
            new TestGame();
            var testGraphicsDevice = new TestGraphicsDevice();
            var system = new TilemapRenderSystem(testGraphicsDevice);
            system.Initialize(new ComponentManager());
        }
    }
}