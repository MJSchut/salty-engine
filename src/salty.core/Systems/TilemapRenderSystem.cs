using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace salty.core.Systems
{
    public class TilemapRenderSystem : EntityDrawSystem
    {
        private readonly OrthographicCamera _camera;
        private TiledMapRenderer? _tiledMapRenderer;
        private ComponentMapper<TiledMap>? _tileMap;
        private readonly GraphicsDevice device;

        public TilemapRenderSystem(GraphicsDevice device, OrthographicCamera camera)
            : base(Aspect.All(typeof(TiledMap)))
        {
            this.device = device;
            _camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tileMap = mapperService.GetMapper<TiledMap>();
        }

        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix(Vector2.One);
            _tiledMapRenderer ??= new TiledMapRenderer(device, _tileMap.Get(ActiveEntities.First()));

            _tiledMapRenderer.Draw(transformMatrix);
        }
    }
}