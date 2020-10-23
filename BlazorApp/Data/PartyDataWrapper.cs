using System;
using System.Collections.Generic;
using SadPumpkin.Util.CombatEngine.Actor;
using SadPumpkin.Util.CombatEngine.Item;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class PartyDataWrapper
    {
        public static string DataPath(string userId, uint partyId) => $"parties/{userId}/{partyId}.json";

        public event EventHandler Updated;

        private uint _partyId;
        private uint _day;
        private TimeOfDay _time;
        private uint _gold;
        private List<PlayerCharacter> _characters;
        private List<IItem> _inventory;
        private Character _calamity;
        private bool _calamityDefeated;

        public uint PartyId
        {
            get => _partyId;
            set
            {
                _partyId = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public uint Day
        {
            get => _day;
            set
            {
                _day = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public TimeOfDay Time
        {
            get => _time;
            set
            {
                _time = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public uint Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<PlayerCharacter> Characters
        {
            get => _characters;
            set
            {
                _characters = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<IItem> Inventory
        {
            get => _inventory;
            set
            {
                _inventory = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public Character Calamity
        {
            get => _calamity;
            set
            {
                _calamity = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CalamityDefeated
        {
            get => _calamityDefeated;
            set
            {
                _calamityDefeated = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string GetDataPath(string userId) => DataPath(userId, _partyId);

        public PartyDataWrapper()
            : this(0u, null, null, null)
        {
        }

        public PartyDataWrapper(
            uint partyId,
            IReadOnlyCollection<PlayerCharacter> characters,
            IReadOnlyCollection<IItem> inventory,
            Character calamity)
        {
            _partyId = partyId;
            _day = 0;
            _time = TimeOfDay.Morning;
            _gold = 100;
            _characters = characters != null
                ? new List<PlayerCharacter>(characters)
                : new List<PlayerCharacter>();
            _inventory = inventory != null
                ? new List<IItem>(inventory)
                : new List<IItem>();
            _calamity = calamity;
        }
    }
}