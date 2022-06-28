using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Components;
using salty.core.Components.EntityComponent;

namespace salty.core.Systems
{
    [With(typeof(Transform2), typeof(PlayerComponent))]
    public class CameraControlSystem : AEntitySetSystem<float>
    {
        private World _world;
        public CameraControlSystem(World world) : base(world)
        {
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            var playerComponent = entity.Get<Transform2>();
            var orthographicCamera = World.Get<OrthographicCamera>();
            orthographicCamera?.LookAt(playerComponent?.Position ?? new Vector2());
        }
    }
}