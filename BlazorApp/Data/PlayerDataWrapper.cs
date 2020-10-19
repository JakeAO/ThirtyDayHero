using System;
using System.Collections.Generic;

namespace SadPumpkin.Games.ThirtyDayHero.BlazorApp.Data
{
    public class PlayerDataWrapper
    {
        public static string DataPath(string userId) => $"players/{userId}.json";

        public uint ActivePartyId;
        public List<uint> PastPartyIds;

        public string GetDataPath(string userId) => DataPath(userId);

        public PlayerDataWrapper()
            : this(0u, null)
        {
        }

        public PlayerDataWrapper(
            uint activePartyId,
            IReadOnlyCollection<uint> pastParties)
        {
            ActivePartyId = activePartyId;
            PastPartyIds = pastParties != null
                ? new List<uint>(pastParties)
                : new List<uint>();
        }

        public void SetActiveParty(uint partyId)
        {
            if (ActivePartyId != 0u)
            {
                PastPartyIds.Add(ActivePartyId);
            }

            ActivePartyId = partyId;
        }
    }
}