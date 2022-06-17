using System;
using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using FluentAssertions;
using MonoGame.Extended;
using salty.core.Components;
using salty.core.Systems;
using Xunit;

namespace salty.core.tests
{
    public class SetPositionSystemTests
    {
        [Fact]
        public void SetPositionSystem_CanBe_Initialized()
        {
            new SetPositionSystem(new World());
        }
        
        [Theory,AutoMoqData]
        public void SetPositionSystem_SetsTransform2Position(
            World world, (float, float) initialPosition, (int, int) endPosition)
        {
            var entity = world.CreateEntity();
            entity.Set(new Transform2(initialPosition.Item1, initialPosition.Item2));
            entity.Set(new SetPositionComponent(endPosition.Item1, endPosition.Item2));
            
            var runner = new DefaultParallelRunner(1);
            var system = new SequentialSystem<float>(new SetPositionSystem(world));
            world.Set<IParallelRunner>(runner);
            system.Update(1.0f);

            var output = entity.Get<Transform2>();
            output.Position.X.Should().Be(endPosition.Item1);
            output.Position.Y.Should().Be(endPosition.Item2);
        }

        [Theory, AutoMoqData]
        public void SetPositionSystem_ConsumesSetPositionComponent(
            World world, (float, float) initialPosition, (int, int) endPosition)
        {
            var entity = world.CreateEntity();
            entity.Set(new Transform2(initialPosition.Item1, initialPosition.Item2));
            entity.Set(new SetPositionComponent(endPosition.Item1, endPosition.Item2));

            var runner = new DefaultParallelRunner(1);
            var system = new SequentialSystem<float>(new SetPositionSystem(world));
            world.Set<IParallelRunner>(runner);
            system.Update(1.0f);
            Action act = () => entity.Get<SetPositionComponent>();
            act.Should().Throw<IndexOutOfRangeException>();
        }
    }
}