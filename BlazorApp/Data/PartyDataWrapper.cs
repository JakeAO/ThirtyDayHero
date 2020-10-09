using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ThirtyDayHero;

namespace BlazorApp.Data
{
    public class PartyDataWrapper
    {
        public static string DataPath(string userId, Guid partyId) => $"parties/{userId}/{partyId}.json";

        public Guid Id;
        public uint Day;
        public uint Time;
        public List<PlayerCharacter> Characters;
        public List<IItem> Inventory;

        [JsonIgnore] public IEnumerable<IWeapon> Weapons => Inventory.Where(x => x is Weapon).Cast<IWeapon>();
        [JsonIgnore] public IEnumerable<IArmor> Armors => Inventory.Where(x => x is Armor).Cast<IArmor>();
        
        public string GetDataPath(string userId) => DataPath(userId, Id);

        public PartyDataWrapper()
            : this(Guid.Empty, null, null)
        {
        }

        public PartyDataWrapper(
            Guid id,
            IReadOnlyCollection<PlayerCharacter> characters,
            IReadOnlyCollection<IItem> inventory)
        {
            Id = id;
            Day = 0;
            Time = 0;
            Characters = characters != null
                ? new List<PlayerCharacter>(characters)
                : new List<PlayerCharacter>();
            Inventory = inventory != null
                ? new List<IItem>(inventory)
                : new List<IItem>();
        }
    }
}