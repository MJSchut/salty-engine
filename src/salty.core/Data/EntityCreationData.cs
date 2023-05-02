using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace salty.core.Data
{
    public class EntityCreationData
    {
        public readonly Dictionary<string, Func<Vector2, bool>> EntityActions;

        public EntityCreationData(Dictionary<string, Func<Vector2, bool>> entityActions)
        {
            EntityActions = entityActions;
        }
    }
}