using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;
using salty.core.Components;
using salty.core.Components.Input;
using salty.core.Components.Interactables;

namespace salty.core.Systems.Interactables
{
    [With(typeof(CursorTriggerComponent), typeof(CollisionComponent))]
    public class CursorInteractableSystem : AEntitySetSystem<float>
    {
        private World _world;

        public CursorInteractableSystem(World world) : base(world)
        {
            _world = world;
        }
        
        protected override void Update(float state, in Entity entity)
        {
            var keyBoard = _world.Get<KeyboardComponent>();

            if (!keyBoard.PressedThisFrame(Keys.Space))
                return;
            
            var collisionData = entity.Get<CollisionComponent>();
            var collidingWith = collisionData.IsCollidingWith;

            foreach (var target in collidingWith)
            {
                if (target.Has<CursorTargetComponent>())
                    target.Get<CursorTargetComponent>().OnInteract();
            }
        }
    }
}