using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace salty.core.Systems
{
    public class RenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private ComponentMapper<Transform2>? _transformMapper;
        private ComponentMapper<Sprite>? _spriteMapper;
        
        
        public RenderSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera)
            : base(Aspect.All(typeof(Sprite), typeof(Transform2)))
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }

        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix(Vector2.One);
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper?.Get(entity);
                var sprite = _spriteMapper?.Get(entity);

                _spriteBatch.Draw(sprite, transform);
            }

            _spriteBatch.End();
        }
    }
}