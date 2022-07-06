using System;
using System.Collections.Generic;
using DefaultEcs;
using Microsoft.Xna.Framework;

namespace salty.core.Components
{
    public class CollisionComponent
    {
                
        /// <summary>
        /// the last position for this component that was valid.
        /// </summary>
        public Vector2 LastValidPosition { get; set; } = Vector2.Zero;
        
        public bool IsColliding { get; set; }

        public HashSet<Entity> IsCollidingWith { get; set; } = new ();

        public bool IsSolid { get; set; } = true;
        
        public float X { get; set; }
        public float Y { get; set; }
        
        public float Width { get; }
        public float Height { get; }
        
        public float XOffset { get; set; }
        public float YOffset { get; set; }
        
        
        public float XMin
        {
            get => X + XOffset;
        }
        public float XMax
        {
            get => X + Width  + XOffset;
        }
        
        public float XMid
        {
            get => X + Width/2  + XOffset;
        }
        
        public float YMin
        {
            get => Y + YOffset;
        }
        
        public float YMax
        {
            get => Y + Height + YOffset;
        }
        
        public float YMid
        {
            get => Y + Height/2 + YOffset;
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
            
            XOffset = -width/2;
            YOffset = -height/2;
        }
        
        public CollisionComponent(float x, float y, float width, float height,
        float? xOffset = null, float? yOffset = null)
        {
            if (width <= 0)
                throw new ArgumentException("Width cannot be less than or equal to 0");
            if (height <= 0)
                throw new ArgumentException("Height cannot be less than or equal to 0");
            
            X = x;
            Y = y;
            Width = width;
            Height = height;
            
            XOffset = xOffset ?? -width/2;
            YOffset = yOffset ?? -height/2;
        }

        public CollisionComponent(float x, float y, float width, float height, bool isSolid = false)
        {
            if (width <= 0)
                throw new ArgumentException("Width cannot be less than or equal to 0");
            if (height <= 0)
                throw new ArgumentException("Height cannot be less than or equal to 0");
            
            X = x;
            Y = y;
            Width = width;
            Height = height;
            
            XOffset = -width/2;
            YOffset = -height/2;
            IsSolid = isSolid;
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