using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using FluentAssertions;
using salty.core.Components.EntityComponent;
using salty.core.Messages;
using salty.core.Systems.Gameplay;
using Xunit;

namespace salty.core.tests
{
    public class PlayerMoneySystemTests
    {
        [Theory]
        [AutoMoqData]
        public void PlayerMoneySystem_Updates_PlayerWalletBasedOnAddMoneyMessages(
            World world, int newValue)
        {
            var player = world.CreateEntity();
            player.Set(new PlayerWalletComponent());

            var runner = new DefaultParallelRunner(1);
            var system = new SequentialSystem<float>(
                new PlayerMoneySystem(world));
            world.Set<IParallelRunner>(runner);
            world.Publish(new AddMoneyMessage(newValue));
            system.Update(1.0f);

            var moneySystem = player.Get<PlayerWalletComponent>();
            moneySystem.Value.Should().Be(newValue);
        }
    }
}