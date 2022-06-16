using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using salty.core.Components;

namespace salty.core.Systems
{
    public class CameraControlSystem : EntityUpdateSystem
    {
        private ComponentMapper<OrthographicCamera>? _cameraMapper;
        private ComponentMapper<PlayerComponent>? _playerComponentMapper;
        private ComponentMapper<Transform2>? _transformMapper;

        public CameraControlSystem() : base(Aspect.One(typeof(OrthographicCamera), typeof(PlayerComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _cameraMapper = mapperService.GetMapper<OrthographicCamera>();
            _playerComponentMapper = mapperService.GetMapper<PlayerComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            Transform2? playerComponent = null;
            OrthographicCamera? orthographicCamera = null;

            foreach (var entity in ActiveEntities)
            {
                if (_cameraMapper != null && _cameraMapper.Has(entity))
                    orthographicCamera = _cameraMapper.Get(entity);
                if (_playerComponentMapper != null && _playerComponentMapper.Has(entity))
                    playerComponent = _transformMapper?.Get(entity);
            }

            orthographicCamera?.LookAt(playerComponent?.Position ?? new Vector2());
        }
    }
}