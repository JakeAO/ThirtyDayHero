using System;
using System.Text;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Abilities;
using SadPumpkin.Util.CombatEngine.CharacterClasses;
using SadPumpkin.Util.CombatEngine.Item;
using SadPumpkin.Util.CombatEngine.Item.Armors;
using SadPumpkin.Util.CombatEngine.Item.Weapons;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public static class TooltipGenerator
    {
        public static string GetTooltip(ICharacterClass characterClass)
        {
            IPlayerClass asPlayerClass = characterClass as IPlayerClass;

            StringBuilder sb = new StringBuilder(50);
            if (!string.IsNullOrWhiteSpace(characterClass.Desc))
                sb.Append("\"").Append(characterClass.Desc).AppendLine("\"");
            if (asPlayerClass != null)
            {
                if (asPlayerClass.WeaponProficiency != WeaponType.Invalid)
                {
                    sb.AppendLine("Weapon Proficiencies: ");
                    bool any = false;
                    sb.Append("   ");
                    foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
                    {
                        if (weaponType == WeaponType.Invalid)
                            continue;
                        if (asPlayerClass.WeaponProficiency.HasFlag(weaponType))
                        {
                            if (any)
                                sb.Append(" | ");
                            any = true;
                            sb.Append(weaponType);
                        }
                    }
                    sb.AppendLine();
                }
                if (asPlayerClass.ArmorProficiency != ArmorType.Invalid)
                {
                    sb.AppendLine("Armor Proficiencies: ");
                    bool any = false;
                    sb.Append("   ");
                    foreach (ArmorType armorType in Enum.GetValues(typeof(ArmorType)))
                    {
                        if (armorType == ArmorType.Invalid)
                            continue;
                        if (asPlayerClass.ArmorProficiency.HasFlag(armorType))
                        {
                            if (any)
                                sb.Append(" | ");
                            any = true;
                            sb.Append(armorType);
                        }
                    }
                    sb.AppendLine();
                }
            }
            if (characterClass.IntrinsicDamageModification.Count > 0)
            {
                sb.AppendLine("Damage Modifiers: ");
                foreach (var dmgModKvp in characterClass.IntrinsicDamageModification)
                {
                    sb.Append("   ").Append(dmgModKvp.Key).Append(": ").Append(dmgModKvp.Value).AppendLine("%");
                }
            }
            if (characterClass.AbilitiesPerLevel.Count > 0)
            {
                sb.AppendLine("Learned Abilities: ");
                foreach (var abilitiesForLevel in characterClass.AbilitiesPerLevel)
                {
                    uint level = abilitiesForLevel.Key;
                    foreach (var ability in abilitiesForLevel.Value)
                    {
                        sb.Append("   [Lvl ").Append(level).Append("] ").AppendLine(ability.Name);
                    }
                }
            }
            return sb.ToString();
        }
        
        public static string GetTooltip(IAbility ability)
        {
            if (ability == null) throw new ArgumentNullException(nameof(ability));
            StringBuilder sb = new StringBuilder(50);
            if (!string.IsNullOrWhiteSpace(ability.Desc))
                sb.Append("\"").Append(ability.Desc).AppendLine("\"");
            sb.Append("Speed: ").AppendLine(ability.Speed.ToString());
            sb.Append("Target: ").AppendLine(ability.Target.Description);
            sb.Append("Effect: ").AppendLine(ability.Effect.Description);
            if (!string.IsNullOrWhiteSpace(ability.Cost.Description()))
                sb.Append("Cost: ").AppendLine(ability.Cost.Description());
            return sb.ToString();
        }

        public static string GetTooltip(ItemDefinition itemDefinition)
        {
            IItem asItem = itemDefinition.Item;
            IWeapon asWeapon = itemDefinition.Item as IWeapon;
            IArmor asArmor = itemDefinition.Item as IArmor;

            StringBuilder sb = new StringBuilder(50);
            if (!string.IsNullOrWhiteSpace(asItem.Desc))
                sb.Append("\"").Append(asItem.Desc).AppendLine("\"");
            sb.Append("Type: ").Append(asItem.ItemType);
            if (asWeapon != null)
            {
                sb.Append(" (").Append(asWeapon.WeaponType).Append(")");
            }
            else if (asArmor != null)
            {
                sb.Append(" (").Append(asArmor.ArmorType).Append(")");
            }
            sb.AppendLine();
            if (asWeapon?.AttackAbility != null)
            {
                sb.AppendLine("Attack: ");
                sb.Append("   Speed: ").AppendLine(asWeapon.AttackAbility.Speed.ToString());
                sb.Append("   Effect: ").AppendLine(asWeapon.AttackAbility.Effect.Description);
            }
            else if (asArmor?.DamageModifiers.Count > 0)
            {
                sb.AppendLine("Damage Modifiers: ");
                foreach (var dmgModKvp in asArmor.DamageModifiers)
                {
                    sb.Append("   ").Append(dmgModKvp.Key).Append(": ").Append(dmgModKvp.Value).AppendLine("%");
                }
            }
            if (asItem.AddedAbilities.Count > 0)
            {
                sb.AppendLine("Added Abilities: ");
                foreach (var ability in asItem.AddedAbilities)
                {
                    sb.Append("   ").AppendLine(ability.Name);
                    sb.Append("      ").AppendLine(ability.Desc);
                    sb.Append("      Effect: ").AppendLine(ability.Effect.Description);
                }
            }
            return sb.ToString();
        }
    }
}