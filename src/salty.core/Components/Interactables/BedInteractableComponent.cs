using DefaultEcs;
using salty.core.Messages;

namespace salty.core.Components.Interactables
{
    public class BedInteractableComponent : CursorTargetComponent
    {
        private World _world;
        
        public BedInteractableComponent(World world)
        {
            _world = world;
        }
        
        public override void OnInteract()
        {
            var timeComponent = _world.Get<WorldTimeComponent>();
            timeComponent.Tick(60*24);
        }
    }
}