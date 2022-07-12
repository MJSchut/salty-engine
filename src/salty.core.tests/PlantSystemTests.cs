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
    public class PlantSystemTests
    {
        [Theory, AutoMoqData]
        public void PlantSystem_Updates_PlantAgeBasedOnNextDayMessages(World world,
            int daysToMature)
        {
            var plant = world.CreateEntity();
            var newPlantComponent = new PlantComponent();
            newPlantComponent.DaysToMature = daysToMature;
            plant.Set(newPlantComponent);
            
            var runner = new DefaultParallelRunner(1);
            var system = new SequentialSystem<float>(
                new PlantSystem(world));
            world.Set<IParallelRunner>(runner);
            
            world.Publish(new NextDayMessage());
            system.Update(1.0f);

            var plantComponent = plant.Get<PlantComponent>();
            plantComponent.CurrentStage.Should().Be(1);
        }
        
        [Theory, AutoMoqData]
        public void PlantSystem_Updates_FullyGrownGetsSetAndCurrentStageMaxesOut(World world,
            int daysToMature)
        {
            var plant = world.CreateEntity();
            var newPlantComponent = new PlantComponent();
            newPlantComponent.DaysToMature = daysToMature;
            plant.Set(newPlantComponent);
            
            var runner = new DefaultParallelRunner(1);
            var system = new SequentialSystem<float>(
                new PlantSystem(world));
            world.Set<IParallelRunner>(runner);

            for (int i = 0; i < daysToMature + 5; i++)
            {
                world.Publish(new NextDayMessage());
                system.Update(1.0f);
            }
            
            var plantComponent = plant.Get<PlantComponent>();
            plantComponent.CurrentStage.Should().Be(daysToMature - 1);
            plantComponent.FullyGrown.Should().BeTrue();
        }
    }
}