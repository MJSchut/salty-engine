using DefaultEcs;
using salty.core.Systems;
using Xunit;

namespace salty.core.tests
{
    public class CameraControlSystemTests
    {
        [Fact]
        public void CameraControlSystem_CanBe_Initialized()
        {
            new CameraControlSystem(new World());
        }
    }
}