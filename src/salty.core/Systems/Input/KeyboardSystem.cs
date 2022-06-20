using DefaultEcs;
using DefaultEcs.System;
using MonoGame.Extended.Tiled;
using salty.core.Components.Input;

namespace salty.core.Systems.Input
{
    public class KeyboardSystem : AComponentSystem<float, KeyboardComponent>
    {
        public KeyboardSystem(World world) : base(world)
        {
        }
        
        protected override void Update(float state, ref KeyboardComponent component)
        {
            component.Update();
        }
    }
}