namespace salty.core.Components
{
    public class WorldTimeComponent
    {
        private int _hour = 8;
        private int _minute;
        private float _minuteFraction;

        public int DayNumber = 1;
        public bool RollToNextDay;

        public int Minute
        {
            get => _minute;
            private set
            {
                _minute = value;
                if (_minute >= 60)
                {
                    _minute -= 60;
                    Hour += 1;
                }
            }
        }

        public int Hour
        {
            get => _hour;
            private set
            {
                _hour = value;
                if (_hour >= 24)
                {
                    _hour -= 24;
                    DayNumber++;
                    RollToNextDay = true;
                }
            }
        }

        public override string ToString()
        {
            return $"Day{DayNumber}; {Hour.ToString()}:{Minute.ToString()}";
        }

        public void Tick(float deltaTime)
        {
            _minuteFraction += deltaTime;

            if (!(_minuteFraction > 1)) return;

            while (_minuteFraction > 1)
            {
                _minuteFraction -= 1;
                Minute += 1;
            }
        }
    }
}