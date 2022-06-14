using MonoGame.Extended;
using MonoGame.Extended.Entities;
using salty.core.Systems;
using Xunit;

namespace salty.core.tests
{
    public class RenderSystemTests
    {
        [Fact]
        public void RenderSystem_CanBe_Initialized()
        {
            new TestGame();
            var testGraphicsDevice = new TestGraphicsDevice();
            var renderSystem = new RenderSystem(testGraphicsDevice, new OrthographicCamera(testGraphicsDevice));

            renderSystem.Initialize(new ComponentManager());

            var tilemapRenderSystem =
                new TilemapRenderSystem(testGraphicsDevice, new OrthographicCamera(testGraphicsDevice));
            tilemapRenderSystem.Initialize(new ComponentManager());
        }
    }
}