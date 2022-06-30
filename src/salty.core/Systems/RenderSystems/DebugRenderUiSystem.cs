using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using salty.core.Components;
using salty.core.Components.Debugging;

namespace salty.core.Systems.RenderSystems
{
    public class DebugRenderUiSystem : AComponentSystem<float, DebugRenderUiComponent>
    {
        private readonly OrthographicCamera _camera;
        private readonly GraphicsDevice _device;
        private readonly SpriteBatch _spriteBatch;

        public DebugRenderUiSystem(World world, GraphicsDevice graphicsDevice, OrthographicCamera camera) : base(world)
        {
            _device = graphicsDevice;
            _camera = camera;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        protected override void Update(float state, ref DebugRenderUiComponent component)
        {
            //UI
            _spriteBatch.Begin();
            
            _spriteBatch.End();
        }
    }
}