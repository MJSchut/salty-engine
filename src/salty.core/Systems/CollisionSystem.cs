using System;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MonoGame.Extended;
using salty.core.Components;
using salty.core.Components.EntityComponent;

namespace salty.core.Systems
{
    [With(typeof(CollisionComponent), typeof(Transform2), typeof(ActorComponent))]
    public class CollisionSystem : AEntitySetSystem<float>
    {
        private EntitySet collidableEntities;
        
        public CollisionSystem(World world, IParallelRunner runner) : base(world, runner)
        { 
            collidableEntities =
                World.GetEntities()
                    .With<CollisionComponent>()
                    .With<Transform2>()
                    .AsSet();
        }
        
        protected override void Update(float elapsedTime, in Entity entity)
        {
            var transform = entity.Get<Transform2>();
            var collisionComponent = entity.Get<CollisionComponent>();
            var actorComponent = entity.Get<ActorComponent>();
            
            if (float.IsNaN(transform.Position.X))
                transform.Position = actorComponent.LastValidPosition;
            
            collisionComponent.IsColliding = false;
            collisionComponent.X = transform.Position.X;
            collisionComponent.Y = transform.Position.Y;

            var entities = collidableEntities.GetEntities();

            // get collision
            foreach (var collidableEntity in entities)
            {
                if (collidableEntity == entity)
                    continue;
                
                var otherCollisionComponent = collidableEntity.Get<CollisionComponent>();
                collisionComponent.IsColliding = collisionComponent.CollidesWith(otherCollisionComponent);

                if (collisionComponent.IsColliding)
                    break;
            }
            
            // prevent intersection
            if (collisionComponent.IsColliding)
            {
                transform.Position = actorComponent.LastValidPosition;
                return;
            }
            actorComponent.LastValidPosition = transform.Position;
        }
    }
}