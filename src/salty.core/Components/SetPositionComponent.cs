using salty.core.Systems;

namespace salty.core.Components
{
    /// <summary>
    /// Add to an entity to set it's position once, will then be consumed by the <see cref="SetPositionSystem"/>
    /// </summary>
    public class SetPositionComponent
    {
        public float x { get; }
        public float y { get; }
        public SetPositionComponent(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}