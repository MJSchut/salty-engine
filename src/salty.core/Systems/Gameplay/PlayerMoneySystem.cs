using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using salty.core.Components.EntityComponent;
using salty.core.Messages;

namespace salty.core.Systems.Gameplay
{
    [With(typeof(PlayerWalletComponent))]
    public class PlayerMoneySystem : ISystem<float>
    {
        private World _world;
        public PlayerMoneySystem(World world)
        {
            _world = world;
            _world.Subscribe(this);
        }
        
        [Subscribe]
        public void On(in AddMoneyMessage message)
        {
            var wallet = _world.GetAll<PlayerWalletComponent>().ToArray()[0];
            wallet.Value += message.Value;
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