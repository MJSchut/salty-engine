using DefaultEcs;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using salty.core.Systems;
using salty.core.Systems.RenderSystems;
using Xunit;

namespace salty.core.tests
{
    public class RenderSystemTests
    {
        [Fact]
        public void RenderSystems_CanBe_Initialized()
        {
            new TestGame();
            var testGraphicsDevice = new TestGraphicsDevice();
            new RenderSystem(new World(), new SpriteBatch(testGraphicsDevice), new OrthographicCamera(testGraphicsDevice));
            new TilemapRenderSystem(new World(), testGraphicsDevice, new OrthographicCamera(testGraphicsDevice));
        }
    }
}