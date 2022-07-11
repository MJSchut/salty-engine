using System.Text;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Sprites;
using salty.core.Components;
using salty.core.Components.Debugging;
using salty.core.Components.EntityComponent;

namespace salty.core.Systems.RenderSystems
{
    public class DebugRenderUiSystem : AComponentSystem<float, DebugRenderUiComponent>
    {
        private readonly World _world;
        private readonly SpriteBatch _spriteBatch;
        private readonly BitmapFont _font;

        public DebugRenderUiSystem(World world, SpriteBatch spriteBatch, BitmapFont font) : base(world)
        {
            _world = world;
            _spriteBatch = spriteBatch;
            _font = font;
        }

        protected override void Update(float state, ref DebugRenderUiComponent component)
        {
            if (!component.IsRendering)
                return;

            var timeString = "";
            if (_world.Has<WorldTimeComponent>())
                timeString = _world.Get<WorldTimeComponent>().ToString();

            var walletString = "";
            if (_world.GetAll<PlayerWalletComponent>().Length >= 1)
            {
                var wallet = _world.GetAll<PlayerWalletComponent>().ToArray()[0];
                walletString = $"${wallet.Value.ToString()}";
            }
            
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.DrawString(_font, timeString, Vector2.One, Color.White);
            _spriteBatch.DrawString(_font, walletString, new Vector2(1, 32), Color.White);
            _spriteBatch.End();
        }
    }
}