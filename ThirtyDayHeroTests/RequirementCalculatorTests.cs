using System.Collections.Generic;
using NUnit.Framework;
using ThirtyDayHero;

namespace ThirtyDayHeroTests
{
    [TestFixture]
    public static class RequirementCalculatorTests
    {
        [TestCase(ExpectedResult = true, TestName = "NoRequirement always returns true.")]
        public static bool NoRequirement_Tests()
        {
            IRequirementCalc requirementCalc = NoRequirements.Instance;
            ICharacter dummyCharacter = new Character(
                0u, 0u, string.Empty,
                NullClass.Instance,
                new StatMap(),
                new EquipMap());

            return requirementCalc.MeetsRequirement(dummyCharacter);
        }

        [TestCase(100u, 100u, ExpectedResult = false, TestName = "CriticalHealthRequirement returns false at 100% hp.")]
        [TestCase(50u, 100u, ExpectedResult = false, TestName = "CriticalHealthRequirement returns false at 50% hp.")]
        [TestCase(25u, 100u, ExpectedResult = false, TestName = "CriticalHealthRequirement returns false at 25% hp.")]
        [TestCase(20u, 100u, ExpectedResult = true, TestName = "CriticalHealthRequirement returns true at 20% hp.")]
        [TestCase(10u, 100u, ExpectedResult = true, TestName = "CriticalHealthRequirement returns true at 10% hp.")]
        public static bool CriticalHealthRequirement_Tests(uint hp, uint hpMax)
        {
            IRequirementCalc requirementCalc = CriticalHealthRequirement.Instance;
            ICharacter dummyCharacter = new Character(
                0u, 0u, string.Empty,
                NullClass.Instance,
                new StatMap(new Dictionary<StatType, uint>()
                {
                    {StatType.HP, hp},
                    {StatType.HP_Max, hpMax}
                }),
                new EquipMap());

            return requirementCalc.MeetsRequirement(dummyCharacter);
        }

        [TestCase(WeaponType.Sword, WeaponType.Sword, ExpectedResult = true, TestName = "Sword matches Sword requirement")]
        [TestCase(WeaponType.Sword, WeaponType.Axe, ExpectedResult = false, TestName = "!Sword fails Sword requirement.")]
        [TestCase(WeaponType.Sword | WeaponType.Axe, WeaponType.Sword, ExpectedResult = true, TestName = "Sword matches Sword|Axe requirement.")]
        [TestCase(WeaponType.Sword | WeaponType.Axe, WeaponType.Axe, ExpectedResult = true, TestName = "Axe matches Sword|Axe requirement.")]
        [TestCase(WeaponType.Sword | WeaponType.Axe, WeaponType.Bow, ExpectedResult = false, TestName = "!Sword fails Sword|Axe requirement.")]
        [TestCase(WeaponType.Invalid, WeaponType.Invalid, ExpectedResult = true, TestName = "Invalid matches Invalid requirement.")]
        [TestCase(WeaponType.Invalid, WeaponType.Axe, ExpectedResult = false, TestName = "!Invalid fails Invalid requirement.")]
        [TestCase(WeaponType.Axe, WeaponType.Invalid, ExpectedResult = false, TestName = "Invalid fails Axe requirement.")]
        [TestCase(WeaponType.Invalid | WeaponType.Axe, WeaponType.Invalid, ExpectedResult = true, TestName = "Invalid matches Axe|Invalid requirement.")]
        public static bool EquippedWeaponRequirement_Tests(WeaponType requirement, WeaponType equipped)
        {
            IRequirementCalc requirementCalc = new EquippedWeaponRequirement(requirement);
            ICharacter dummyCharacter = new Character(
                0u, 0u, string.Empty,
                NullClass.Instance,
                new StatMap(),
                new EquipMap(
                    new Weapon(0u, string.Empty, string.Empty, equipped, null, null)));

            return requirementCalc.MeetsRequirement(dummyCharacter);
        }

        [TestCase(ArmorType.Light, ArmorType.Light, ExpectedResult = true, TestName = "Light matches Light requirement")]
        [TestCase(ArmorType.Light, ArmorType.Medium, ExpectedResult = false, TestName = "!Light fails Light requirement.")]
        [TestCase(ArmorType.Light | ArmorType.Medium, ArmorType.Light, ExpectedResult = true, TestName = "Light matches Light|Medium requirement.")]
        [TestCase(ArmorType.Light | ArmorType.Medium, ArmorType.Medium, ExpectedResult = true, TestName = "Medium matches Light|Medium requirement.")]
        [TestCase(ArmorType.Light | ArmorType.Medium, ArmorType.Heavy, ExpectedResult = false, TestName = "!Light fails Light|Medium requirement.")]
        [TestCase(ArmorType.Invalid, ArmorType.Invalid, ExpectedResult = true, TestName = "Invalid matches Invalid requirement.")]
        [TestCase(ArmorType.Invalid, ArmorType.Light, ExpectedResult = false, TestName = "!Invalid fails Invalid requirement.")]
        [TestCase(ArmorType.Light, ArmorType.Invalid, ExpectedResult = false, TestName = "Invalid fails Light requirement.")]
        [TestCase(ArmorType.Invalid | ArmorType.Light, WeaponType.Invalid, ExpectedResult = true, TestName = "Invalid matches Light|Invalid requirement.")]
        public static bool EquippedArmorRequirement_Tests(ArmorType requirement, ArmorType equipped)
        {
            IRequirementCalc requirementCalc = new EquippedArmorRequirement(requirement);
            ICharacter dummyCharacter = new Character(
                0u, 0u, string.Empty,
                NullClass.Instance,
                new StatMap(),
                new EquipMap(
                    armor: new Armor(0u, string.Empty, string.Empty, equipped, null, null)));

            return requirementCalc.MeetsRequirement(dummyCharacter);
        }
    }
}