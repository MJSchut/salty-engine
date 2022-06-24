
using Microsoft.Xna.Framework;

namespace salty.core.Components
{

    
    public class ActorComponent
    {
        
        public ActorComponent(float speed)
        {
            Speed = speed;
        }
        
        /// <summary>
        /// determines how fast an actor can move, should be around 40.
        /// </summary>
        public float Speed { get; }
        
        /// <summary>
        /// the last position for the actor that was valid.
        /// </summary>
        public Vector2 LastValidPosition { get; set; } = Vector2.Zero;
    }
}