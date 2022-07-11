using DefaultEcs;
using salty.core.Components.EntityComponent;
using salty.core.Messages;

namespace salty.core.Components.Interactables
{
    public class PlantInteractableComponent : CursorTargetComponent
    {
        private World _world;
        private Entity _entity;

        public PlantInteractableComponent(World world, Entity thisEntity)
        {
            _entity = thisEntity;
            _world = world;
        }
        
        public override void OnInteract()
        {
            if (_entity.Has<PlantComponent>())
            {
                var plantComponent = _entity.Get<PlantComponent>();
                if (_entity.Has<SellableComponent>())
                {
                    var sellable = _entity.Get<SellableComponent>();
                    _world.Publish(new AddMoneyMessage(sellable.Value));
                }
            }
            
            _entity.Dispose();
        }
    }
}