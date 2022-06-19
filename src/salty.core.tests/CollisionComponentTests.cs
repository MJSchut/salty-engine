using FluentAssertions;
using salty.core.Components;
using Xunit;

namespace salty.core.tests
{
    public class CollisionComponentTests
    {
        [Theory]
        [InlineData(10, 10, 10, 10, 
                    10, 10, 10, 10, true)] // exact overlap
        
        [InlineData(10, 10, 10, 10, 
                    15, 10, 10, 10, true)] // overlap x
        [InlineData(15, 10, 10, 10, 
                    10, 10, 10, 10, true)] // overlap x
        [InlineData(100, 10, 10, 10, 
                    10, 10, 10, 10, false)] // no overlap x
        [InlineData(10, 10, 10, 10, 
                    100, 10, 10, 10, false)] // no overlap x
        [InlineData(19.99, 10, 10, 10, 
                    10, 10, 10, 10, true)] // tiny overlap x
        [InlineData(10, 10, 10, 10, 
                    19.99, 10, 10, 10, true)] // tiny overlap x
        
        [InlineData(10, 10, 15, 10, 
                    10, 10, 10, 10, true)] // overlap y
        [InlineData(10, 10, 10, 10, 
                    10, 10, 15, 10, true)] // overlap y
        [InlineData(10, 10, 100, 10, 
                    10, 10, 10, 10, false)] // no overlap y
        [InlineData(10, 10, 10, 10, 
                    10, 10, 100, 10, false)] // no overlap y
        [InlineData(10, 10, 19.99, 10, 
                    10, 10, 10, 10, true)] // tiny overlap y
        [InlineData(10, 10, 10, 10, 
                    10, 10, 19.99, 10, true)] // tiny overlap y
        
        public void CollidesWith_Behaves_As_Expected(
            float x1, float width1, float y1, float height1,
            float x2, float width2, float y2, float height2,
            bool result)
        {
            var collisionComponent1 = new CollisionComponent(x1, y1, width1, height1);
            var collisionComponent2 = new CollisionComponent(x2, y2, width2, height2);
            collisionComponent1.CollidesWith(collisionComponent2).Should().Be(result);
        }
    }
}