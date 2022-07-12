
using System;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using FluentAssertions;
using MonoGame.Extended;
using salty.core.Components.EntityComponent;
using salty.core.Components.Movement;
using salty.core.Messages;
using salty.core.Systems;
using salty.core.Systems.Gameplay;
using salty.core.Systems.Input;
using salty.core.Systems.Movement;
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