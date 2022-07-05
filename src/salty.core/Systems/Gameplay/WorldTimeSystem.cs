using DefaultEcs;
using DefaultEcs.System;
using MonoGame.Extended.Tiled;
using salty.core.Components;
using salty.core.Messages;

namespace salty.core.Systems.Gameplay
{
    public class WorldTimeSystem : AComponentSystem<float, WorldTimeComponent>
    {
        private World _world;

        public WorldTimeSystem(World world) : base(world)
        {
            _world = world;
        }
        
        protected override void Update(float state, ref WorldTimeComponent component)
        {
            component.Tick(state);

            if (component.RollToNextDay)
            {
                _world.Publish(new NextDayMessage());
                component.RollToNextDay = false;
            }
                
        }
    }
}