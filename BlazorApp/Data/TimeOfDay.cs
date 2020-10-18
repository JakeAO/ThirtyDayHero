using System;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    [Flags]
    public enum TimeOfDay
    {
        Morning = 1,
        Evening = 2,
        Night = 4
    }
}