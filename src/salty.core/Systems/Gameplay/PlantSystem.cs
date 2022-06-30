using DefaultEcs;
using DefaultEcs.System;
using MonoGame.Extended.Sprites;
using salty.core.Components.EntityComponent;
using salty.core.Messages;

namespace salty.core.Systems.Gameplay
{
    [With(typeof(PlantComponent))]
    public class PlantSystem : ISystem<float>
    {
        private World _world;
        public PlantSystem(World world)
        {
            _world = world;
            _world.Subscribe(this);
        }

        [Subscribe]
        public void On(in NextDayMessage _)
        {
            // update all plants
            var plants = _world.GetEntities().With<PlantComponent>().With<AnimatedSprite>().AsSet();

            foreach (var plant in plants.GetEntities())
            {
                if (plant.Has<PlantComponent>() == false || plant.Has<AnimatedSprite>() == false)
                    continue;
                
                var plantComponent = plant.Get<PlantComponent>();
                var spriteComponent = plant.Get<AnimatedSprite>();
    
                plantComponent.CurrentStage += 1;
                spriteComponent.Play(plantComponent.CurrentStage.ToString());
                spriteComponent.Update(1);
            }
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