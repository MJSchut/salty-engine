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
        private ComponentMapper<ActorComponent>? _actorMapper;
        private ComponentMapper<PlayerComponent>? _playerMapper;
        private ComponentMapper<Transform2>? _transformMapper;

        public PlayerControlSystem() : base(Aspect.All(typeof(PlayerComponent), typeof(ActorComponent), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _playerMapper = mapperService.GetMapper<PlayerComponent>();
            _actorMapper = mapperService.GetMapper<ActorComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var player = _playerMapper?.Get(entityId);
            var transform = _transformMapper?.Get(entityId);
            var actorComponent = _actorMapper?.Get(entityId);

            if (player == null || transform == null || actorComponent == null)
                return;

            var speed = actorComponent.Speed * gameTime.GetElapsedSeconds();

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