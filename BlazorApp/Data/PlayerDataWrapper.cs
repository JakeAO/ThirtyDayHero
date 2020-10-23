using System;
using System.Collections.Generic;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class PlayerDataWrapper
    {
        public static string DataPath(string userId) => $"players/{userId}.json";

        public event EventHandler Updated;

        private uint _activePartyId;
        private List<uint> _pastPartyIds;

        public uint ActivePartyId
        {
            get => _activePartyId;
            set
            {
                _activePartyId = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<uint> PastPartyIds
        {
            get => _pastPartyIds;
            set
            {
                _pastPartyIds = value;
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string GetDataPath(string userId) => DataPath(userId);

        public PlayerDataWrapper()
            : this(0u, null)
        {
        }

        public PlayerDataWrapper(
            uint activePartyId,
            IReadOnlyCollection<uint> pastParties)
        {
            _activePartyId = activePartyId;
            _pastPartyIds = pastParties != null
                ? new List<uint>(pastParties)
                : new List<uint>();
        }

        public void SetActiveParty(uint partyId)
        {
            if (_activePartyId != 0u)
            {
                _pastPartyIds.Add(_activePartyId);
            }

            _activePartyId = partyId;

            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}