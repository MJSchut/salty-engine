namespace salty.core.Components
{
    public class StateComponent
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public enum State
        {
            Idle,
            Walking
        }

        public State CurrentState = State.Idle;

        /// <summary>
        ///     the direction that the actor is facing, used for sprites etc.
        /// </summary>
        public Direction Facing = Direction.Left;
    }
}