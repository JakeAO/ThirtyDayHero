using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class PartyDataWrapper
    {
        public static string DataPath(string userId, Guid partyId) => $"parties/{userId}/{partyId}.json";

        public Guid Id;
        public uint Day;
        public TimeOfDay Time;
        public uint Gold;
        public List<PlayerCharacter> Characters;
        public List<IItem> Inventory;
        public uint CalamityId;
        
        public string GetDataPath(string userId) => DataPath(userId, Id);

        public PartyDataWrapper()
            : this(Guid.Empty, null, null, 0u)
        {
        }

        public PartyDataWrapper(
            Guid id,
            IReadOnlyCollection<PlayerCharacter> characters,
            IReadOnlyCollection<IItem> inventory,
            uint calamityId)
        {
            Id = id;
            Day = 0;
            Time = TimeOfDay.Morning;
            Gold = 100;
            Characters = characters != null
                ? new List<PlayerCharacter>(characters)
                : new List<PlayerCharacter>();
            Inventory = inventory != null
                ? new List<IItem>(inventory)
                : new List<IItem>();
            CalamityId = calamityId;
        }
    }
}