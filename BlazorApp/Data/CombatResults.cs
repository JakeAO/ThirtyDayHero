using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class CombatResults
    {
        public bool Success { get; private set; }
        public uint ExpReward { get; private set; }

        public static CombatResults CreateSuccess(IReadOnlyCollection<ICharacterActor> enemies, PartyDataWrapper party)
        {
            uint GetStatTotal(IReadOnlyCollection<ICharacterActor> actors)
            {
                uint total = 0u;
                foreach (ICharacterActor actor in actors)
                {
                    for (StatType statType = StatType.STR; statType <= StatType.CHA; statType++)
                    {
                        total += actor.Stats[statType];
                    }
                }

                return total;
            }

            uint enemyStatTotal = GetStatTotal(enemies);
            uint playerStatTotal = GetStatTotal(party.Characters);
            float modifier = enemyStatTotal / (float) playerStatTotal;

            float baseExp = enemies.Count * 100f;
            float expReward = baseExp * modifier;

            return new CombatResults()
            {
                Success = true,
                ExpReward = (uint) Math.Ceiling(expReward)
            };
        }

        public static CombatResults CreateFailure()
        {
            return new CombatResults()
            {
                Success = false,
                ExpReward = 0u
            };
        }
    }
}