using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MonoGame.Extended;
using salty.core.Components;

namespace salty.core.Systems.Physics
{
    [With(typeof(CollisionComponent), typeof(Transform2))]
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

            if (float.IsNaN(transform.Position.X))
                transform.Position = collisionComponent.LastValidPosition;
            
            collisionComponent.IsColliding = false;
            collisionComponent.X = transform.Position.X;
            collisionComponent.Y = transform.Position.Y;
            collisionComponent.IsCollidingWith.Clear();
            collisionComponent.IsCollidingWithSolid = false;

            var entities = collidableEntities.GetEntities();

            // get collision
            foreach (var collidableEntity in entities)
            {
                if (collidableEntity == entity)
                    continue;
                
                var otherCollisionComponent = collidableEntity.Get<CollisionComponent>();
                collisionComponent.IsColliding = collisionComponent.CollidesWith(otherCollisionComponent);

                if (collisionComponent.IsColliding)
                {
                    collisionComponent.IsCollidingWithSolid = otherCollisionComponent.IsSolid;
                    collisionComponent.IsCollidingWith.Add(collidableEntity);
                    break;
                }
            }
            
            // prevent intersection
            if (collisionComponent.IsColliding && collisionComponent.IsSolid && collisionComponent.IsCollidingWithSolid)
            {
                transform.Position = collisionComponent.LastValidPosition;
                return;
            }
            collisionComponent.LastValidPosition = transform.Position;
        }
    }
}