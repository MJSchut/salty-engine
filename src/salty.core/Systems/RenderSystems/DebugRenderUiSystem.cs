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
            
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.DrawString(_font, timeString, Vector2.One, Color.White);
            _spriteBatch.End();
        }
    }
}