using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using salty.core;

namespace salty.game
{
    public class GameState : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameWorld _gameWorld;
        private OrthographicCamera _camera;

        public GameState()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.SynchronizeWithVerticalRetrace = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        
        public void OnResize(Object sender, EventArgs e)
        {

            if (_graphics.PreferredBackBufferWidth != _graphics.GraphicsDevice.Viewport.Width ||
                _graphics.PreferredBackBufferHeight != _graphics.GraphicsDevice.Viewport.Height)
            {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
                _graphics.ApplyChanges();
            }
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1600, 900);
            _camera = new OrthographicCamera(viewportAdapter);
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _gameWorld = new GameWorld(_graphics.GraphicsDevice, Content, _camera);
            
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
        }
        

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            _gameWorld.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _gameWorld.Draw(gameTime);
            
            base.Draw(gameTime);
        }
    }
}
