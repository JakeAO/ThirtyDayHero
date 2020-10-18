using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class RodDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_ROD);

        public static readonly IWeapon FireWand = new Weapon(
            IdTracker.Next,
            "Fire Wand", "A wand enchanted with the power of elemental Fire.",
            WeaponType.Rod,
            AttackDefinitions.Attack_STR_Fixed,
            new[]
            {
                SpellDefinitions.Spell_Ember
            });

        public static readonly IWeapon IceWand = new Weapon(
            IdTracker.Next,
            "Frost Wand", "A wand enchanted with the power of elemental Ice.",
            WeaponType.Rod,
            AttackDefinitions.Attack_STR_Fixed,
            new[]
            {
                SpellDefinitions.Spell_Snowball
            });

        public static readonly IWeapon StoneWand = new Weapon(
            IdTracker.Next,
            "Stone Wand", "A wand enchanted with the power of elemental Earth.",
            WeaponType.Rod,
            AttackDefinitions.Attack_STR_Fixed,
            new[]
            {
                SpellDefinitions.Spell_Spike
            });

        public static readonly IWeapon WindWand = new Weapon(
            IdTracker.Next,
            "Wind Wand", "A wand enchanted with the power of elemental Air.",
            WeaponType.Rod,
            AttackDefinitions.Attack_STR_Fixed,
            new[]
            {
                SpellDefinitions.Spell_Gale
            });
    }
}