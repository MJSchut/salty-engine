using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Util;

namespace salty.core.Components.AI
{
    public class RandomWanderAiComponent : AiComponent
    {
        private const float RandomMin = 3.0f;
        private const float RandomMax = 6.0f;
        private float _newDirectionTimer = 0.5f;
        private Vector2 _oldDirection = Vector2.Zero;

        public override Vector2 Update(Vector2 originalPosition, float speed, float timePassed)
        {
            var direction = _newDirectionTimer > 0 ? _oldDirection : GetNewDirection();

            if (_newDirectionTimer > 0)
                _newDirectionTimer -= timePassed;
            else
                _newDirectionTimer = RandomProvider.Next(RandomMin, RandomMax);

            _oldDirection = direction;
            
            return originalPosition + direction * speed * timePassed;
        }

        private static Vector2 GetNewDirection()
        {
            return RandomProvider.Next() < 0.6f ? Vector2.Zero : new Vector2(RandomProvider.Next(-1, 2), RandomProvider.Next(-1, 2)).NormalizedCopy();
        }
    }
}