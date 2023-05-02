using DefaultEcs;
using DefaultEcs.System;
using salty.core.Components.EntityComponent;
using salty.core.Messages;

namespace salty.core.Systems.Gameplay
{
    [With(typeof(PlayerWalletComponent))]
    public class PlayerMoneySystem : ISystem<float>
    {
        private readonly World _world;

        public PlayerMoneySystem(World world)
        {
            _world = world;
            _world.Subscribe(this);
        }

        public void Dispose()
        {
        }

        public void Update(float state)
        {
        }

        public bool IsEnabled { get; set; }

        [Subscribe]
        public void On(in AddMoneyMessage message)
        {
            var wallet = _world.GetAll<PlayerWalletComponent>().ToArray()[0];
            wallet.Value += message.Value;
        }
    }
}