using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using salty.core.Components;

namespace salty.core.Systems
{
    public class PlayerControlSystem : EntityProcessingSystem
    {
        private ComponentMapper<PlayerComponent>? _playerMapper;
        private ComponentMapper<Transform2>? _transformMapper;

        public PlayerControlSystem() : base(Aspect.All(typeof(PlayerComponent), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _playerMapper = mapperService.GetMapper<PlayerComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }
        
        public override void Process(GameTime gameTime, int entityId)
        {
            var player = _playerMapper?.Get(entityId);
            var transform = _transformMapper?.Get(entityId);
            if (player == null || transform == null)
                return;
            
            var keyboardState = KeyboardExtended.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                transform.Position = new Vector2(transform.Position.X, transform.Position.Y - 10);
            } if (keyboardState.IsKeyDown(Keys.Down))
            {
                transform.Position = new Vector2(transform.Position.X, transform.Position.Y + 10);
            } if (keyboardState.IsKeyDown(Keys.Right))
            {
                transform.Position = new Vector2(transform.Position.X + 10, transform.Position.Y);
            } if (keyboardState.IsKeyDown(Keys.Left))
            {
                transform.Position = new Vector2(transform.Position.X - 10, transform.Position.Y);
            }
        }
    }
}