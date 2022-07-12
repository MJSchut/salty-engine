using System;
using System.Collections.Generic;

namespace salty.core.Components.EntityComponent
{
    public class PlantComponent
    {
        /// <summary>
        /// number of unique growing stages
        /// </summary>
        public int NumberOfStages;

        public bool FullyGrown => CurrentStage == DaysToMature - 1;
        
        /// <summary>
        /// number of days to mature
        /// </summary>
        public int DaysToMature;

        private int _currentStage;
        /// <summary>
        /// current stage of the plant (min 0, max DaysToMature)
        /// </summary>
        public int CurrentStage
        {
            get => _currentStage;
            set => _currentStage = Math.Clamp(value, 0, DaysToMature - 1);
        }

        
    }
}