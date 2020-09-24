using System;

namespace ThirtyDayHero
{
    public interface IEquipMapBuilder
    {
        IEquipMap Generate(Random random);
    }
}