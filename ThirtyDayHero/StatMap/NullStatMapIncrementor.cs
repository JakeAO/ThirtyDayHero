using System;

namespace ThirtyDayHero
{
    public class NullStatMapIncrementor : IStatMapIncrementor
    {
        public static readonly NullStatMapIncrementor Instance = new NullStatMapIncrementor();
        
        public IStatMap Increment(IStatMap statMap, Random random)
        {
            return statMap;
        }
    }
}