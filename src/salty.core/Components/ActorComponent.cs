namespace salty.core.Components
{
    public class ActorComponent
    {
        public float Speed { get; private set; }
        
        public ActorComponent(float speed)
        {
            this.Speed = speed;
        }
    }
}