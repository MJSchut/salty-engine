using System;
using DefaultEcs;
using DefaultEcs.System;
using salty.core.Data;
using salty.core.Messages;

namespace salty.core.Systems.Gameplay
{
    public class ItemPlacingSystem : ISystem<float>
    {
        private readonly EntityCreationData _data;

        public ItemPlacingSystem(World world)
        {
            _data = world.Get<EntityCreationData>();
            world.Subscribe(this);
        }

        public void Dispose()
        {
        }

        public void Update(float state)
        {
        }

        public bool IsEnabled { get; set; }


        [Subscribe]
        public void On(in PlaceItemMessage message)
        {
            _data.EntityActions.TryGetValue(message.ItemPlaced, out var itemPlaced);
            if (itemPlaced == null)
                throw new ArgumentException($"Item {message.ItemPlaced} not found!");
            itemPlaced.Invoke(message.ItemLocation);
        }
    }
}