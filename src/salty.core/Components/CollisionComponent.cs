using System;
using Microsoft.Xna.Framework;

namespace salty.core.Components
{
    public class CollisionComponent
    {
        public bool IsColliding { get; set; }

        public bool IsSolid { get; set; } = true;
        
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; }
        public float Height { get; }
        
        public float XMin
        {
            get => X;
        }
        public float XMax
        {
            get => X + Width;
        }
        
        public float XMid
        {
            get => X + Width/2;
        }
        
        public float YMin
        {
            get => Y;
        }
        
        public float YMax
        {
            get => Y + Height;
        }
        
        public float YMid
        {
            get => Y + Height/2;
        }

        public CollisionComponent(float x, float y, float width, float height)
        {
            if (width <= 0)
                throw new ArgumentException("Width cannot be less than or equal to 0");
            if (height <= 0)
                throw new ArgumentException("Height cannot be less than or equal to 0");
            
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(CollisionComponent other)
        {
            // If one rectangle is on left side of other
            if (XMin > other.XMax || other.XMin > XMax)
                return false;
 
            // If one rectangle is above other
            if (YMin > other.YMax || other.YMin > YMax)
                return false;
 
            return true;
        }
        
        public bool CollidingAtPoint(Vector2 vec)
        {
            return CollidingAtPoint(vec.X, vec.Y);
        }
        
        public bool CollidingAtPoint(float x, float y)
        {
            if (XMin > x && x > XMax)
                return false;

            if (YMin > y && y > YMax)
                return false;
 
            return true;
        }
        
    }
}