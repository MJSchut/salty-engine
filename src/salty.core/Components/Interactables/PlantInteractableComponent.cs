using DefaultEcs;

namespace salty.core.Components.Interactables
{
    public class PlantInteractableComponent : CursorTargetComponent
    {
        private Entity _entity;

        public PlantInteractableComponent(Entity thisEntity)
        {
            _entity = thisEntity;
        }
        
        public override void OnInteract()
        {
            _entity.Dispose();
        }
    }
}