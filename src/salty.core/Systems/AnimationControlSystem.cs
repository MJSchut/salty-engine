using System;
using System.Collections.Generic;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using salty.core.Components;

namespace salty.core.Systems
{
    [With(typeof(StateComponent), typeof(AnimatedSprite))]
    public class AnimationControlSystem : AEntitySetSystem<float>
    {
        public AnimationControlSystem(World world, IParallelRunner runner) : base(world, runner)
        {
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            var state = entity.Get<StateComponent>();
            var sprite = entity.Get<AnimatedSprite>();
            
            var animationFrame = $"{state.CurrentState}{state.Facing}";

            try
            {
                sprite.Play(animationFrame);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}