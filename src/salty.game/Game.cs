using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using salty.core;

namespace salty.game
{
    public class GameState : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GameWorld _gameWorld;

        public GameState()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.SynchronizeWithVerticalRetrace = true;
            _graphics.ApplyChanges();

            _gameWorld = new GameWorld(_graphics.GraphicsDevice);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
