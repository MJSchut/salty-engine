using DefaultEcs;
using DefaultEcs.System;
using DefaultEcs.Threading;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Components.Movement;
using salty.core.Util;

namespace salty.core.Systems.Movement
{
    [With(typeof(Transform2), typeof(RestrictToGridComponent))]
    public class RestrictToGridSystem : AEntitySetSystem<float>
    {
        public RestrictToGridSystem(World world, IParallelRunner runner) : base(world, runner)
        {
            
        }
        
        protected override void Update(float timeElapsed, in Entity entity)
        {
            var restrictToGridComponent = entity.Get<RestrictToGridComponent>();
            var currentPosition = entity.Get<Transform2>();

            currentPosition.Position = new Vector2(
                currentPosition.Position.X.RoundToNearest(restrictToGridComponent.GridSizeX),
                currentPosition.Position.Y.RoundToNearest(restrictToGridComponent.GridSizeY));
        }
    }
}