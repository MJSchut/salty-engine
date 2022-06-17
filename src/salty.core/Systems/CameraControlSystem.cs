using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using salty.core.Components;

namespace salty.core.Systems
{
    [With(typeof(Transform2), typeof(PlayerComponent))]
    public class CameraControlSystem : AEntitySetSystem<float>
    {
        private World world;
        public CameraControlSystem(World world) : base(world)
        {
        }

        protected override void Update(float elapsedTime, in Entity entity)
        {
            Transform2? playerComponent = entity.Get<Transform2>();
            OrthographicCamera? orthographicCamera = World.Get<OrthographicCamera>();
            orthographicCamera?.LookAt(playerComponent?.Position ?? new Vector2());
        }
    }
}