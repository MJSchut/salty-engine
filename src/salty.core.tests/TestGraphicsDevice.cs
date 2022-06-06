using Microsoft.Xna.Framework.Graphics;

namespace salty.core.tests
{
    public class TestGraphicsDevice : GraphicsDevice
    {
        public TestGraphicsDevice() 
            : base(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters())
        {
        }
    }
}