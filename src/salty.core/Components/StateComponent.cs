namespace salty.core.Components
{
    public class StateComponent
    {
        public enum State
        {
            Idle, Walking
        }
        
        public enum Direction
        {
            Up, Down, Left, Right
        }
        
        /// <summary>
        /// the direction that the actor is facing, used for sprites etc.
        /// </summary>
        public Direction Facing = Direction.Left;
        public State CurrentState = State.Idle;
    }
}