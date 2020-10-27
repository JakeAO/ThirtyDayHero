using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.CharacterClasses;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions
{
    public static class EnemyGroupDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.ENEMY_GROUP);

        public static readonly EnemyGroup Slimes = new EnemyGroup(
            IdTracker.Next,
            RarityCategory.Common,
            "A group of acidic slimes leave a smoking trail as they slowly roll their way across the plains.",
            new[]
            {
                MonsterDefinitions.Blob,
                MonsterDefinitions.Ooze
            });
    }
}