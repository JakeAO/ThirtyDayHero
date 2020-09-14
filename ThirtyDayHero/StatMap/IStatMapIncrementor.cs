using System;

namespace ThirtyDayHero
{
    public interface IStatMapIncrementor
    {
        IStatMap Increment(IStatMap statMap, Random random);
    }
}