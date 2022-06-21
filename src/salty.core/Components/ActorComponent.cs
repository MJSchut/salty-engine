
using Microsoft.Xna.Framework;

namespace salty.core.Components
{
    public class ActorComponent
    {
        
        public ActorComponent(float speed)
        {
            Speed = speed;
        }

        public float Speed { get; }
        public Vector2 LastValidPosition { get; set; } = Vector2.Zero;
    }
}