using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using salty.core.Util;

namespace salty.core.Components.Debugging
{
    public class DebugRenderComponent
    {
        public bool IsRendering = false;

        public DebugRenderComponent(GraphicsDevice device)
        {
            DebugTextureDefault = Texture2DUtils.CreateColoredTexture(
                device, 1, 1, Color.Purple);

            DebugTextureHit = Texture2DUtils.CreateColoredTexture(
                device, 1, 1, Color.Cyan);
        }

        public Texture2D DebugTextureDefault { get; }
        public Texture2D DebugTextureHit { get; }
    }
}