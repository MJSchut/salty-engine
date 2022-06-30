using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using salty.core.Components.EntityComponent;
using salty.core.Components.Input;

namespace salty.core.Systems.Input
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

            var vectorY = 0;
            var vectorX = 0;
            var speed = actorComponent.Speed * elapsedTime;
            
            if (keyboardComponent.IsKeyDown(Keys.Up))
                vectorY = -1;
            else if (keyboardComponent.IsKeyDown(Keys.Down))
                vectorY = 1;
            
            if (keyboardComponent.IsKeyDown(Keys.Right))
                vectorX = 1;
            else if (keyboardComponent.IsKeyDown(Keys.Left))
                vectorX = -1;

            if (vectorX == 0 && vectorY == 0)
                return;
            
            var movementVector = new Vector2(vectorX, vectorY).NormalizedCopy();
            transform.Position += movementVector * speed;
        }
    }
}