using DefaultEcs;

namespace salty.core.Components.Interactables
{
    public class BedInteractableComponent : CursorTargetComponent
    {
        private readonly World _world;

        public BedInteractableComponent(World world)
        {
            _world = world;
        }

        public override void OnInteract()
        {
            var timeComponent = _world.Get<WorldTimeComponent>();
            timeComponent.Tick(60 * 24);
        }
    }
}