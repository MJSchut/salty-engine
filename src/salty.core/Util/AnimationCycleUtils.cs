using MonoGame.Extended.Sprites;

namespace salty.core.Util
{
    public static class AnimationCycleUtils
    {
        public static SpriteSheetAnimationCycle CreateAnimationCycle(int[] frames, bool isLooping = true,
            float frameDuration = 0.1f)
        {
            var cycle = new SpriteSheetAnimationCycle();
            foreach (var f in frames)
                cycle.Frames.Add(new SpriteSheetAnimationFrame(f, frameDuration));
            cycle.IsLooping = isLooping;
            return cycle;
        }
    }
}