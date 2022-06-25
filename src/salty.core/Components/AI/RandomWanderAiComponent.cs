using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Util;

namespace salty.core.Components.AI
{
    public class RandomWanderAiComponent : AiComponent
    {
        private const float randomMin = 3.0f;
        private const float randomMax = 6.0f;
        private float newDirectionTimer = 0.5f;
        private Vector2 oldDirection = Vector2.Zero;
        
        
        public override Vector2 Update(Vector2 originalPosition, float speed, float timePassed)
        {
            var direction = newDirectionTimer > 0 ? oldDirection : GetNewDirection();

            if (newDirectionTimer > 0)
                newDirectionTimer -= timePassed;
            else
                newDirectionTimer = RandomProvider.Next(randomMin, randomMax);

            oldDirection = direction;
            
            return originalPosition + direction * speed * timePassed;
        }

        private Vector2 GetNewDirection()
        {
            return RandomProvider.Next() < 0.6f ? Vector2.Zero : new Vector2(RandomProvider.Next(-1, 2), RandomProvider.Next(-1, 2)).NormalizedCopy();
        }
    }
}