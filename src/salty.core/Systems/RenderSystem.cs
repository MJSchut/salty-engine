using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace salty.core.Systems
{
    [With(typeof(Sprite), typeof(Transform2))]
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
            var sprite = entity.Get<Sprite>();
            var transform = entity.Get<Transform2>();
            
            _spriteBatch.Draw(sprite, transform);
        }
        
        protected override void PostUpdate(float elapsedTime)
        {
            _spriteBatch.End();
        }
    }
}