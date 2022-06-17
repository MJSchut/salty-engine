using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using salty.core.Components;

namespace salty.core.Systems
{
    [With(typeof(PlayerComponent), typeof(Transform2), typeof(ActorComponent))]
    public sealed class PlayerControlSystem : AEntitySetSystem<float>
    {

        public PlayerControlSystem(World world) : base(world)
        {
        }
        
        protected override void Update(float elapsedTime, in Entity entity)
        {
            var player = entity.Get<PlayerComponent>();
            var transform = entity.Get<Transform2>();
            var actorComponent = entity.Get<ActorComponent>();

            if (player == null || transform == null || actorComponent == null)
                return;

            var speed = actorComponent.Speed * elapsedTime;

            var keyboardState = KeyboardExtended.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
                transform.Position = new Vector2(transform.Position.X, transform.Position.Y - speed);
            if (keyboardState.IsKeyDown(Keys.Down))
                transform.Position = new Vector2(transform.Position.X, transform.Position.Y + speed);
            if (keyboardState.IsKeyDown(Keys.Right))
                transform.Position = new Vector2(transform.Position.X + speed, transform.Position.Y);
            if (keyboardState.IsKeyDown(Keys.Left))
                transform.Position = new Vector2(transform.Position.X - speed, transform.Position.Y);
        }
    }
}