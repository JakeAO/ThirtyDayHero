using System;

namespace ThirtyDayHero
{
    [Flags]
    public enum WeaponType
    {
        Invalid = 1,
        
        Sword = 2,
        GreatSword = 4,
        Axe = 8,
        GreatAxe = 16,
        Spear = 32,
        Staff = 64,
        Rod = 128,
        Bow = 245,
        Fist = 512
    }
}