using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class PartyDataWrapper
    {
        public static string DataPath(string userId, uint partyId) => $"parties/{userId}/{partyId}.json";

        public uint PartyId;
        public uint Day;
        public TimeOfDay Time;
        public uint Gold;
        public List<PlayerCharacter> Characters;
        public List<IItem> Inventory;
        public uint CalamityId;
        
        public string GetDataPath(string userId) => DataPath(userId, PartyId);

        public PartyDataWrapper()
            : this(0u, null, null, 0u)
        {
        }

        public PartyDataWrapper(
            uint partyId,
            IReadOnlyCollection<PlayerCharacter> characters,
            IReadOnlyCollection<IItem> inventory,
            uint calamityId)
        {
            PartyId = partyId;
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