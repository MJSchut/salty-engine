using Microsoft.Xna.Framework;

namespace salty.core.Components.AI
{
    public abstract class AiComponent
    {
        public abstract Vector2 Update(Vector2 originalPosition, float speed, float timePassed);
    }
}