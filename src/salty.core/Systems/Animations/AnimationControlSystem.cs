using System;
using System.Collections.Generic;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MonoGame.Extended.Sprites;
using salty.core.Components;

namespace salty.core.Systems.Animations
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
            try
            {
                sprite.Play($"{state.CurrentState.ToString()}{state.Facing.ToString()}");
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}