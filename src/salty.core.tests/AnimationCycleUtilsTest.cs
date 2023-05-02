using System.Linq;
using FluentAssertions;
using MonoGame.Extended.Sprites;
using salty.core.Util;
using Xunit;

namespace salty.core.tests
{
    public class AnimationCycleUtilsTest
    {
        [Theory]
        [AutoMoqData]
        public void CreateAnimationCycle_Contains_InputData(
            int[] frames, bool isLooping, float frameDuration)
        {
            var animationCycle = AnimationCycleUtils.CreateAnimationCycle(frames, isLooping, frameDuration);

            var spriteSheetAnimationFrames =
                frames.Select(f => new SpriteSheetAnimationFrame(f, frameDuration)).ToList();

            animationCycle.Frames.Should().BeEquivalentTo(spriteSheetAnimationFrames);
            animationCycle.IsLooping.Should().Be(isLooping);
        }
    }
}