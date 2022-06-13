using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using salty.core.Systems;
using Xunit;

namespace salty.core.tests
{
    public class PlayerControlSystemTests
    {
        [Fact]
        public void PlayerControlSystem_CanBe_Initialized()
        {
            var system = new PlayerControlSystem();
            system.Initialize(new ComponentManager());
        }
    }
}