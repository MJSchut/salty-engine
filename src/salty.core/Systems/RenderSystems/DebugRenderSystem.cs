using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using salty.core.Components;
using salty.core.Components.Input;
using salty.core.Input;
using salty.core.Util;

namespace salty.core.Systems.RenderSystems
{
    public class DebugRenderSystem : AComponentSystem<float, DebugRenderComponent>
    {
        private readonly OrthographicCamera _camera;
        private readonly GraphicsDevice _device;
        private readonly SpriteBatch _spriteBatch;
        
        public DebugRenderSystem(World world, GraphicsDevice graphicsDevice, OrthographicCamera camera) : base(world)
        {
            _device = graphicsDevice;
            _camera = camera;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }
        
        protected override void Update(float state, ref DebugRenderComponent component)
        {
            var keyboardComponent = World.Get<KeyboardComponent>();
            
            if (keyboardComponent.PressedThisFrame(Keys.OemQuestion))
                component.IsRendering = !component.IsRendering;

            if (component.IsRendering == false)
                return;
            
            var transformMatrix = _camera.GetViewMatrix(Vector2.One);
            var collisionComponents = World.GetAll<CollisionComponent>();
            
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            foreach (var collision in collisionComponents)
            {
                var texture = collision.IsColliding ? component.DebugTextureHit : component.DebugTextureDefault;
                var sprite = new Sprite(texture);
                _spriteBatch.Draw(sprite, new Vector2(collision.XMid, collision.YMid), 0, new Vector2(collision.Width, collision.Height));
            }
            
            _spriteBatch.End();
        }
    }
}