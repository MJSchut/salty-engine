using Microsoft.Xna.Framework;

namespace salty.core.Components.AI
{
    public interface IAiComponent
    {
        public Vector2 Update(Vector2 originalPosition, float timePassed);
    }
}