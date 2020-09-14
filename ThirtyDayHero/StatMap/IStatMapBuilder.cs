using System;

namespace ThirtyDayHero
{
    public interface IStatMapBuilder
    {
        IStatMap Generate(Random random);
    }
}