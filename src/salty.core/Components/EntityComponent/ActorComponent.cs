namespace salty.core.Components.EntityComponent
{
    public class ActorComponent
    {
        public ActorComponent(float speed)
        {
            Speed = speed;
        }

        /// <summary>
        ///     determines how fast an actor can move, should be around 40.
        /// </summary>
        public float Speed { get; }
    }
}