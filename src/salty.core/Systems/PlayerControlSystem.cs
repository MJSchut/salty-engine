using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using salty.core.Components;

namespace salty.core.Systems
{
    public class PlayerControlSystem : EntityProcessingSystem
    {
        public PlayerControlSystem() : base(Aspect.All(typeof(PlayerComponent), typeof(Transform2)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}