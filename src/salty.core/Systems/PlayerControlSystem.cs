using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using salty.core.Components;
using salty.core.Components.Input;
using salty.core.Input;

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
            var keyboardComponent = World.Get<KeyboardComponent>();
            
            var transform = entity.Get<Transform2>();
            var actorComponent = entity.Get<ActorComponent>();
            var speed = actorComponent.Speed * elapsedTime;
            
            if (keyboardComponent.IsKeyDown(Keys.Up))
                transform.Position = new Vector2(transform.Position.X, transform.Position.Y - speed);
            if (keyboardComponent.IsKeyDown(Keys.Down))
                transform.Position = new Vector2(transform.Position.X, transform.Position.Y + speed);
            if (keyboardComponent.IsKeyDown(Keys.Right))
                transform.Position = new Vector2(transform.Position.X + speed, transform.Position.Y);
            if (keyboardComponent.IsKeyDown(Keys.Left))
                transform.Position = new Vector2(transform.Position.X - speed, transform.Position.Y);
        }
    }
}