
using DefaultEcs;
using salty.core.Systems;
using salty.core.Systems.Input;
using Xunit;

namespace salty.core.tests
{
    public class PlayerControlSystemTests
    {
        [Fact]
        public void PlayerControlSystem_CanBe_Initialized()
        {
            new PlayerControlSystem(new World());
        }
    }
}