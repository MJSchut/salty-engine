using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace salty.core.Systems
{
    public class TilemapRenderSystem : AComponentSystem<float, TiledMap>
    {
        private readonly OrthographicCamera _camera;
        private TiledMapRenderer? _tiledMapRenderer;
        private readonly GraphicsDevice _device;

        public TilemapRenderSystem(World world, GraphicsDevice device, OrthographicCamera camera)
            : base(world)
        {
            _device = device;
            _camera = camera;
        }
        
        protected override void Update(float state, ref TiledMap component)
        {
            var transformMatrix = _camera.GetViewMatrix(Vector2.One);
            _tiledMapRenderer ??= new TiledMapRenderer(_device, component);
            _tiledMapRenderer.Draw(transformMatrix);
        }
    }
}