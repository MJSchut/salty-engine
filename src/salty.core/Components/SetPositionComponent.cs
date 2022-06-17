﻿using salty.core.Systems;

namespace salty.core.Components
{
    /// <summary>
    /// Add to an entity to set it's position once, will then be consumed by the <see cref="SetPositionSystem"/>
    /// </summary>
    public class SetPositionComponent
    {
        public float X { get; }
        public float Y { get; }
        public SetPositionComponent(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}