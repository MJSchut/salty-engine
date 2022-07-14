using DefaultEcs;
using DefaultEcs.System;
using salty.core.Data;

namespace salty.core.Systems.Gameplay
{
    public class ItemPlacingSystem : ISystem<float>
    {
        private EntityCreationData _data;
        
        public ItemPlacingSystem(World world)
        {
            _data = world.Get<EntityCreationData>();
        }
        
        public void Dispose()
        {
        }

        public void Update(float state)
        {
        }

        public bool IsEnabled { get; set; }
    }
}