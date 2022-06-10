using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace salty.core.Util
{
    public static class Texture2DUtils
    {
        public static Texture2D CreateColoredTexture(GraphicsDevice device, int width, int height, Color color)
        {
            var texture = new Texture2D(device, width, height);

            var data = new Color[width * height];
            for (var pixel = 0; pixel < data.Length; pixel++)
                data[pixel] = color;

            //set the color
            texture.SetData(data);
            return texture;
        }
    }
}