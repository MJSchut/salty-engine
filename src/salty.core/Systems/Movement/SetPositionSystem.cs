using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Components.Movement;

namespace salty.core.Systems.Movement
{
    [With(typeof(SetPositionComponent), typeof(Transform2))]
    public class SetPositionSystem : AEntitySetSystem<float>
    {
        public SetPositionSystem(World world) : base(world)
        {}
        
        protected override void Update(float timeElapsed, in Entity entity)
        {
            var setPosition = entity.Get<SetPositionComponent>();
            var currentPosition = entity.Get<Transform2>();

            currentPosition.Position = new Vector2(setPosition.X, setPosition.Y);
            entity.Remove<SetPositionComponent>();
        }
    }
}