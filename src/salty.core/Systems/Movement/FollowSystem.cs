using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Components.Movement;

namespace salty.core.Systems.Movement
{
    [With(typeof(FollowComponent), typeof(Transform2))]
    public class FollowSystem : AEntitySetSystem<float>
    {
        public FollowSystem(World world) : base(world)
        {
        }

        protected override void Update(float timeElapsed, in Entity entity)
        {
            var followComponent = entity.Get<FollowComponent>();
            var currentPosition = entity.Get<Transform2>();

            currentPosition.Position = new Vector2(followComponent.FollowTarget.Position.X,
                followComponent.FollowTarget.Position.Y);
        }
    }
}