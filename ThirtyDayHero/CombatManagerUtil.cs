using System.Collections.Generic;
using ThirtyDayHero.CharacterClasses;

namespace ThirtyDayHero
{
    public static class CombatManagerUtil
    {
        public static CombatManager CreateDebugLoadedManager(ICharacterController playerCharacterController)
        {
            IReadOnlyCollection<IPlayerCharacterActor> playerCharacters = new[]
            {
                ClassUtil.CreatePlayerCharacter(10, 1, "Rouche",
                    PlayerCharacterClassDefinitions.SOLDIER,
                    3u),
                ClassUtil.CreatePlayerCharacter(11, 1, "Obrem",
                    PlayerCharacterClassDefinitions.MAGE,
                    3u)
            };
            IReadOnlyCollection<ICharacterActor> enemyCharacters = new[]
            {
                ClassUtil.CreateCharacter(12, 2, "Glob",
                    MonsterCharacterClassDefinitions.OOZE,
                    1),
                ClassUtil.CreateCharacter(13, 2, "Blob",
                    MonsterCharacterClassDefinitions.OOZE,
                    1),
                ClassUtil.CreateCharacter(14, 2, "Slob",
                    MonsterCharacterClassDefinitions.OOZE,
                    1),
            };

            IParty playerParty = new Party.Party(1, playerCharacterController, playerCharacters);
            IParty enemyParty = new Party.Party(2, new RandomCharacterController(), enemyCharacters);

            IReadOnlyCollection<IParty> parties = new[] {playerParty, enemyParty};

            return new CombatManager(parties);
        }
    }
}