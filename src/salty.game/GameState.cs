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
    public class GameState : Game
    {
        private const string GameName = "LittleGarden";
        private const string GameVersion = "pre-alpha";
        private readonly GraphicsDeviceManager _graphics;

        private readonly WindowOptions _windowOptions;
        private string _appDataFolderPath = string.Empty;
        private OrthographicCamera _camera;
        private GameWorld _gameWorld;

        public GameState()
        {
            _graphics = new GraphicsDeviceManager(this);

            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.SynchronizeWithVerticalRetrace = true;

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnResize;

            _windowOptions = new WindowOptions(1600, 900);

            Content.RootDirectory = "Content";
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
            Window.Title = $"{GameName} {GameVersion}";

            InitializeAppDataFolder();
            LoadWindowData();
        }

        private void InitializeAppDataFolder()
        {
            // The folder for the roaming current user 
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string gameFolder = Path.Combine(folder, $"{GameName}");

            // CreateDirectory will check if every folder in path exists and, if not, create them.
            // If all folders exist then CreateDirectory will do nothing.
            Directory.CreateDirectory(gameFolder);

            // Combine the base folder with your specific folder....
            _appDataFolderPath = Path.Combine(gameFolder, $"{GameVersion}");

            // CreateDirectory will check if every folder in path exists and, if not, create them.
            // If all folders exist then CreateDirectory will do nothing.
            Directory.CreateDirectory(_appDataFolderPath);
        }

        private void LoadWindowData()
        {
        }

        protected override void LoadContent()
        {
            var viewportAdapter =
                new BoxingViewportAdapter(Window, GraphicsDevice, _windowOptions.Width, _windowOptions.Height);

            _camera = new OrthographicCamera(viewportAdapter);
            _camera.Zoom = 3;
            _gameWorld = new GameWorld(_graphics.GraphicsDevice, Content, _camera);

            _graphics.PreferredBackBufferWidth = _windowOptions.Width;
            _graphics.PreferredBackBufferHeight = _windowOptions.Height;
            _graphics.ApplyChanges();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GraphicsDevice.Clear(Color.GhostWhite);

            _gameWorld.Update(gameTime);

            base.Update(gameTime);
        }
    }
}