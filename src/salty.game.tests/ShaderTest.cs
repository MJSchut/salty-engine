using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using salty.core.Data;

namespace salty.game
{
    public class ShaderTest : Game
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDeviceManager _graphics;
        
        public ShaderTest()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.SynchronizeWithVerticalRetrace = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;

            Content.RootDirectory = "DebugContent";
            IsMouseVisible = true;
        }

        public void OnResize(object sender, EventArgs e)
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


        private void LoadWindowData()
        {
        }

        protected override void LoadContent()
        {
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Console.WriteLine(gameTime.GetElapsedSeconds());
            GraphicsDevice.Clear(Color.GhostWhite);

            base.Draw(gameTime);
        }
    }
}