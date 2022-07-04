using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;
using salty.core.Components;
using salty.core.Components.Debugging;
using salty.core.Components.Input;
using salty.core.Messages;

namespace salty.core.Systems.Input
{
    public class DebugControlSystem : AComponentSystem<float, DebugControlComponent>
    {
        private readonly World _world;
        
        public DebugControlSystem(World world) : base(world)
        {
            _world = world;
        }
        
        protected override void Update(float state, ref DebugControlComponent component)
        {
            var keyboard = _world.Get<KeyboardComponent>();
            
            if (keyboard.PressedThisFrame(Keys.OemPlus))
            {
                var timeComponent = _world.Get<WorldTimeComponent>();
                timeComponent.Tick(60*24);
            }
                
        }
    }
}