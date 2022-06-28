using System;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MonoGame.Extended;
using salty.core.Components;
using salty.core.Components.AI;
using salty.core.Components.EntityComponent;

namespace salty.core.Systems
{
    [With(typeof(Transform2), typeof(AiComponent), typeof(ActorComponent), typeof(StateComponent))]
    public class AiSystem : AEntitySetSystem<float>
    {

        public AiSystem(World world, IParallelRunner runner) : base(world, runner)
        {
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            var transform = entity.Get<Transform2>();
            var aiSystem = entity.Get<AiComponent>();
            var state = entity.Get<StateComponent>();
            var actor = entity.Get<ActorComponent>();

            CollisionComponent? collision = null;
            if (entity.Has<CollisionComponent>())
                collision = entity.Get<CollisionComponent>();

            var oldPosition = transform.Position;
            var newPosition = aiSystem.Update(transform.Position, actor.Speed, elapsedTime, collision?.IsColliding ?? false);
            var difference = oldPosition - newPosition;
            
            state.CurrentState = newPosition == oldPosition ? 
                StateComponent.State.Idle :
                StateComponent.State.Walking;
            
            if (difference.X < 0)
                state.Facing = StateComponent.Direction.Right;
            else if (difference.X > 0)
                state.Facing = StateComponent.Direction.Left;
            else if (difference.Y > 0)
                state.Facing = StateComponent.Direction.Up;
            else if (difference.Y < 0)
                state.Facing = StateComponent.Direction.Down;
            
            transform.Position = newPosition;
        }
    }
}