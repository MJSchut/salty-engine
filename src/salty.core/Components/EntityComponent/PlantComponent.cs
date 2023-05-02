using System;

namespace salty.core.Components.EntityComponent
{
    public class PlantComponent
    {
        private int _currentStage;

        /// <summary>
        ///     number of days to mature
        /// </summary>
        public int DaysToMature = 1;

        /// <summary>
        ///     number of unique growing stages
        /// </summary>
        public int NumberOfStages;

        public bool FullyGrown => CurrentStage == DaysToMature - 1;

        /// <summary>
        ///     current stage of the plant (min 0, max DaysToMature)
        /// </summary>
        public int CurrentStage
        {
            get => _currentStage;
            set => _currentStage = Math.Clamp(value, 0, DaysToMature - 1);
        }
    }
}