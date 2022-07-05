using DefaultEcs;
using DefaultEcs.System;
using salty.core.Components.Interactables;

namespace salty.core.Systems.Interactables
{
    public class CursorInteractableSystem : AComponentSystem<float, CursorTriggerComponent>
    {
        private World _world;

        public CursorInteractableSystem(World world) : base(world)
        {
            _world = world;
        }
        
        protected override void Update(float state, ref CursorTriggerComponent component)
        {
            var targets = _world.GetAll<CursorTargetComponent>();
            foreach (var target in targets)
            {
                target.OnInteract();
            }
        }
    }
}