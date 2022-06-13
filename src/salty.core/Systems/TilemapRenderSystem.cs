using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace salty.core.Systems
{
    public class TilemapRenderSystem : EntityDrawSystem
    {
        private ComponentMapper<TiledMap>? _tileMap;
        private TiledMapRenderer _tiledMapRenderer;
        private GraphicsDevice device;
        
        public TilemapRenderSystem(GraphicsDevice device) 
            : base(Aspect.All(typeof(TiledMap)))
        {
            this.device = device;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _tileMap = mapperService.GetMapper<TiledMap>();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                _tiledMapRenderer = new TiledMapRenderer(device, _tileMap.Get(entity));
                _tiledMapRenderer.Draw();
            }
        }
    }
}