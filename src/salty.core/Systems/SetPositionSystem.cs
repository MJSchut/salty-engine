using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using salty.core.Components;

namespace salty.core.Systems
{
    
    public class SetPositionSystem : EntityProcessingSystem
    {
        private ComponentMapper<SetPositionComponent>? _setPositionMapper;
        private ComponentMapper<Transform2>? _transformMapper;
        
        public SetPositionSystem() : base(Aspect.All(typeof(SetPositionComponent), typeof(Transform2)))
        {}

        public override void Initialize(IComponentMapperService mapperService)
        {
            _setPositionMapper = mapperService.GetMapper<SetPositionComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var setPosition = _setPositionMapper?.Get(entityId);
            var currentPosition = _transformMapper?.Get(entityId);

            if (setPosition == null || currentPosition == null)
                return;

            currentPosition.Position = new Vector2(setPosition.x, setPosition.y);
            
            _setPositionMapper?.Delete(entityId);
        }
    }
}