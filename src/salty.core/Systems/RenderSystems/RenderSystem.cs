using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace salty.core.Systems.RenderSystems
{
    [With(typeof(Transform2))]
    [WithEither(typeof(Sprite), typeof(AnimatedSprite))]
    public class RenderSystem : AEntitySetSystem<float>
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly OrthographicCamera _camera;
        private const int SortingDivision = 10000;


        public RenderSystem(World world, SpriteBatch spriteBatch, OrthographicCamera camera)
            : base(world)
        {
            _spriteBatch = spriteBatch;
            _camera = camera;
        }
        
        protected override void PreUpdate(float elapsedTime)
        {
            var transformMatrix = _camera.GetViewMatrix(Vector2.One);
            _spriteBatch.Begin(
                transformMatrix: transformMatrix, 
                samplerState: SamplerState.PointClamp, 
                sortMode: SpriteSortMode.FrontToBack);
        }

        protected override void Update(float state, in Entity entity)
        {
            var transform = entity.Get<Transform2>();
            if (entity.Has<Sprite>())
            {
                var sprite = entity.Get<Sprite>();
                sprite.Depth = transform.Position.Y/SortingDivision;
                _spriteBatch.Draw(sprite, transform.Position, 0, Vector2.One );
                return;
            }
            
            var animatedSprite = entity.Get<AnimatedSprite>();
            animatedSprite.Update(state);
            animatedSprite.Depth = transform.Position.Y/SortingDivision;
            _spriteBatch.Draw(animatedSprite, transform);
        }
        
        protected override void PostUpdate(float elapsedTime)
        {
            _spriteBatch.End();
        }
    }
}