using System;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class SkillCheck
    {
        private static readonly Random RANDOM = new Random();

        public StatType AttackerStat { get; }
        public double AttackerValue { get; }
        public StatType DefenderStat { get; }
        public double DefenderValue { get; }

        public double Chance { get; }

        public SkillCheck(
            StatType attackerStat, double attackerValue,
            StatType defenderStat, double defenderValue)
        {
            AttackerStat = attackerStat;
            AttackerValue = attackerValue;
            DefenderStat = defenderStat;
            DefenderValue = defenderValue;

            // TODO BAD
            Chance = 0f;
            for (int i = 0; i < 1000; i++)
            {
                if (GetResult())
                {
                    Chance += 0.001f;
                }
            }
        }

        public bool GetResult()
        {
            double attackerRoll = RANDOM.NextDouble() * AttackerValue;
            double defenderRoll = RANDOM.NextDouble() * DefenderValue;

            return attackerRoll > defenderRoll;
        }
    }
}