using System;
using System.Collections.Generic;
using SadPumpkin.Games.ThirtyDayHero.BlazorApp.Pages.States;
using SadPumpkin.Games.ThirtyDayHero.Core;
using SadPumpkin.Games.ThirtyDayHero.Core.Decorators;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.Item;
using SadPumpkin.Util.Context;
using SadPumpkin.Util.StateMachine.States;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.States
{
    public class CreatePartyState : BlazorState
    {
        private static readonly Random RANDOM = new Random();
        
        public readonly Guid NewPartyGuid = Guid.NewGuid();
        public IReadOnlyCollection<PlayerCharacter> GeneratedCharacters => _generatedCharacters;
        public IReadOnlyCollection<IItem> GeneratedInventory => _generatedInventory;
        public Character Calamity => _calamity;

        private readonly List<PlayerCharacter> _generatedCharacters = new List<PlayerCharacter>(5);
        private readonly List<IItem> _generatedInventory = new List<IItem>(10);
        private Character _calamity = null;
        
        public override Type RenderType => typeof(CreatePartyStatePage);

        public override void PerformSetup(Context context, IState previousState)
        {
            base.PerformSetup(context, previousState);

            // Generate Calamity
            EnemyDefinition calamityType = HackUtil.GetRandomCalamityClass();
            _calamity = ClassUtil.CreateCharacter(
                calamityType.Id,
                (uint) Guid.NewGuid().GetHashCode(),
                calamityType.NameGenerator.GetName(),
                calamityType.CharacterClass,
                30u);
            
            // Generate Potential Party Members
            uint partyId = (uint) NewPartyGuid.GetHashCode();
            for (int i = 0; i < 5; i++)
            {
                uint id = (uint) (i + 1);
                PlayerClassDefinition randomClass = HackUtil.GetRandomPlayerClass();
                string randomName = randomClass.NameGenerator.GetName();
                PlayerCharacter playerCharacter = ClassUtil.CreatePlayerCharacter(id, partyId, randomName, randomClass.PlayerClass);
                _generatedCharacters.Add(playerCharacter);
            }

            // Generate (Unequipped) Starting Equipment
            int weaponCount = RANDOM.Next(3);
            for (int i = 0; i < weaponCount; i++)
            {
                if (HackUtil.GetRandomWeapon() is var newWeapon)
                {
                    _generatedInventory.Add(newWeapon.Item);
                }
            }

            int armorCount = RANDOM.Next(3);
            for (int i = 0; i < armorCount; i++)
            {
                if (HackUtil.GetRandomArmor() is var newArmor)
                {
                    _generatedInventory.Add(newArmor.Item);
                }
            }

            int itemCount = RANDOM.Next(5);
            for (int i = 0; i < itemCount; i++)
            {
                if (HackUtil.GetRandomItem() is var newItem)
                {
                    _generatedInventory.Add(newItem.Item);
                }
            }
        }
    }
}