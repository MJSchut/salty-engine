using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace salty.core.Systems
{
    [With(typeof(Transform2))]
    [WithEither(typeof(Sprite), typeof(AnimatedSprite))]
    public class RenderSystem : AEntitySetSystem<float>
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;


        public RenderSystem(World world, GraphicsDevice graphicsDevice, OrthographicCamera camera)
            : base(world)
        {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _camera = camera;
        }
        
        protected override void PreUpdate(float elapsedTime)
        {
            var transformMatrix = _camera.GetViewMatrix(Vector2.One);
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);
        }

        protected override void Update(float state, in Entity entity)
        {
            var transform = entity.Get<Transform2>();
            if (entity.Has<Sprite>())
            {
                var sprite = entity.Get<Sprite>();
                _spriteBatch.Draw(sprite, transform);
                return;
            }
            
            var animatedSprite = entity.Get<AnimatedSprite>();
            animatedSprite.Update(state);
            _spriteBatch.Draw(animatedSprite, transform);
        }
        
        protected override void PostUpdate(float elapsedTime)
        {
            _spriteBatch.End();
        }
    }
}