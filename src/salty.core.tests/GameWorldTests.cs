using Microsoft.Xna.Framework.Graphics;
using Moq;
using Xunit;

namespace salty.core.tests
{
    public class GameWorldTests
    {
        [Fact]
        public void GameWorld_Contains_RenderSystem()
        {
            
            var gameWorld = new GameWorld(new Mock<GraphicsDevice>().Object);

        }
    }
}