using MonoGame.Extended;

namespace salty.core.Components.Movement
{
    public class FollowComponent
    {
        public Transform2 FollowTarget;
        
        public FollowComponent(Transform2 followTarget)
        {
            FollowTarget = followTarget;
        }
    }
}