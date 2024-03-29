﻿using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace salty.core.Systems.RenderSystems
{
    public class TilemapRenderSystem : AComponentSystem<float, TiledMap>
    {
        private readonly OrthographicCamera _camera;
        private readonly GraphicsDevice _device;
        private TiledMapRenderer? _tiledMapRenderer;

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