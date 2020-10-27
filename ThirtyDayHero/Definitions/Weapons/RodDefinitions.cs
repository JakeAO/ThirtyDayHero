using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Abilities;
using SadPumpkin.Util.CombatEngine.Item.Weapons;
using SadPumpkin.Util.CombatEngine.StatMap;

namespace SadPumpkin.Games.ThirtyDayHero.Core.Definitions.Weapons
{
    public static class RodDefinitions
    {
        public static readonly TrackableIdGenerator IdTracker = new TrackableIdGenerator(ConstantIds.WEAPON_ROD);

        public static readonly IWeapon FireWandItem = new Weapon(
            IdTracker.Next,
            "Fire Wand", "A wand enchanted with the power of elemental Fire.",
            WeaponType.Rod,
            AttackDefinitions.NewAttack(90, DamageType.Normal, StatType.STR, 0.2f, 0.2f),
            new[]
            {
                SpellDefinitions.Spell_Ember
            });

        public static readonly ItemDefinition<IWeapon> FireWand = new ItemDefinition<IWeapon>(
            "assets/weapon/rod/staff_8.png",
            150,
            RarityCategory.Common,
            FireWandItem);

        public static readonly IWeapon IceWandItem = new Weapon(
            IdTracker.Next,
            "Frost Wand", "A wand enchanted with the power of elemental Ice.",
            WeaponType.Rod,
            AttackDefinitions.NewAttack(90, DamageType.Normal, StatType.STR, 0.2f, 0.2f),
            new[]
            {
                SpellDefinitions.Spell_Snowball
            });

        public static readonly ItemDefinition<IWeapon> IceWand = new ItemDefinition<IWeapon>(
            "assets/weapon/rod/staff_8.png",
            150,
            RarityCategory.Common,
            IceWandItem);

        public static readonly IWeapon StoneWandItem = new Weapon(
            IdTracker.Next,
            "Stone Wand", "A wand enchanted with the power of elemental Earth.",
            WeaponType.Rod,
            AttackDefinitions.NewAttack(90, DamageType.Normal, StatType.STR, 0.2f, 0.2f),
            new[]
            {
                SpellDefinitions.Spell_Spike
            });

        public static readonly ItemDefinition<IWeapon> StoneWand = new ItemDefinition<IWeapon>(
            "assets/weapon/rod/staff_8.png",
            150,
            RarityCategory.Common,
            StoneWandItem);

        public static readonly IWeapon WindWandItem = new Weapon(
            IdTracker.Next,
            "Wind Wand", "A wand enchanted with the power of elemental Air.",
            WeaponType.Rod,
            AttackDefinitions.NewAttack(90, DamageType.Normal, StatType.STR, 0.2f, 0.2f),
            new[]
            {
                SpellDefinitions.Spell_Gale
            });

        public static readonly ItemDefinition<IWeapon> WindWand = new ItemDefinition<IWeapon>(
            "assets/weapon/rod/staff_8.png",
            150,
            RarityCategory.Common,
            WindWandItem);
    }
}