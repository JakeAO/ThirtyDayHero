using System;
using System.Collections.Generic;

namespace BlazorApp.Data
{
    public class PlayerDataWrapper
    {
        public static string DataPath(string userId) => $"players/{userId}.json";

        public Guid ActiveParty;
        public List<Guid> PastParties;
        
        public string GetDataPath(string userId) => DataPath(userId);

        public PlayerDataWrapper()
            : this(Guid.Empty, null)
        {
        }

        public PlayerDataWrapper(
            Guid activeParty,
            IReadOnlyCollection<Guid> pastParties)
        {
            ActiveParty = activeParty;
            PastParties = pastParties != null
                ? new List<Guid>(pastParties)
                : new List<Guid>();
        }

        public void SetActiveParty(Guid partyId)
        {
            if (ActiveParty != Guid.Empty)
            {
                PastParties.Add(ActiveParty);
            }

            ActiveParty = partyId;
        }
    }
}