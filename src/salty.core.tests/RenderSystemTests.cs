using Microsoft.Xna.Framework;
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
            var renderSystem = new RenderSystem(testGraphicsDevice);

            renderSystem.Initialize(new ComponentManager());
        }
    }
}