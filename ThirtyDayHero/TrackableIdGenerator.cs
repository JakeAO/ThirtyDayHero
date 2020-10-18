using System;

namespace SadPumpkin.Games.ThirtyDayHero.Core
{
    public class TrackableIdGenerator
    {
        public uint Min { get; }
        public uint Max { get; }
        
        public uint Current => _current;

        public uint Next
        {
            get
            {
                if (_current > Max)
                    throw new IndexOutOfRangeException($"Current value of TrackableIdGenerator has exceeded the maximum value of {Max}");

                uint next = _current;
                _current++;
                return next;
            }
        }

        private uint _current;

        public TrackableIdGenerator(uint min, uint count = 1000)
        {
            Min = min;
            Max = min + count - 1;
            _current = min;
        }
    }
}