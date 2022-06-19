using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MonoGame.Extended;
using salty.core.Components;

namespace salty.core.Systems
{
    [With(typeof(CollisionComponent), typeof(Transform2), typeof(ActorComponent))]
    public class CollisionSystem : AEntitySetSystem<float>
    {
        public CollisionSystem(World world, IParallelRunner runner) : base(world, runner)
        {
        }
        
        protected override void Update(float elapsedTime, in Entity entity)
        {
            var transform = entity.Get<Transform2>();
            var collisionComponent = entity.Get<CollisionComponent>();
            
            collisionComponent.IsColliding = false;
            collisionComponent.X = transform.Position.X;
            collisionComponent.Y = transform.Position.Y;

            var collidableEntities =
                World.GetEntities()
                    .With<CollisionComponent>()
                    .With<Transform2>()
                    .AsSet();

            foreach (var collidableEntity in collidableEntities.GetEntities())
            {
                var otherCollisionComponent = collidableEntity.Get<CollisionComponent>();
                collisionComponent.IsColliding = collisionComponent.CollidesWith(otherCollisionComponent);
            }

        }
    }
}