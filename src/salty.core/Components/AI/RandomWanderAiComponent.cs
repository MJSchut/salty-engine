using System;
using Microsoft.Xna.Framework;

namespace salty.core.Components.AI
{
    public class RandomWanderAiComponent : IAiComponent
    {
        public Vector2 Update(Vector2 originalPosition, float timePassed)
        {
            var rand = new Random();
            return originalPosition + new Vector2(rand.Next(2) - 1, rand.Next(2) - 1);
        }
    }
}