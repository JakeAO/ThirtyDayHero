using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThirtyDayHero;

namespace ConsoleApp
{
    public class ConsoleCharacterController : ICharacterController
    {
        public void SelectAction(IInitiativeActor activeEntity, IReadOnlyDictionary<uint, IAction> availableActions, Action<uint> selectAction)
        {
            // Display Action Options
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[Active: {activeEntity.Name} (#{activeEntity.Id}; P:{activeEntity.Party})]");
            foreach (var kvp in availableActions)
            {
                IAbility ability = kvp.Value.Ability;
                if (!kvp.Value.Available)
                {
                    sb.Append("-INACTIVE- ");
                }
                sb.Append($"[{kvp.Key}] {ability.Name}");
                if (kvp.Value.Targets != null && kvp.Value.Targets.Count > 0)
                {
                    sb.Append($" Target: {string.Join(", ", kvp.Value.Targets.Select(x => $"{x.Name} ({x.Id})"))})");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb);

            // Get Action Choice
            uint actionId = 0;
            do
            {
                string stringActionId = Console.ReadLine();
                uint.TryParse(stringActionId, out actionId);
            } while (!availableActions.TryGetValue(actionId, out IAction _));

            Console.WriteLine($"====> {actionId}");

            // Send Action Response
            selectAction(actionId);
        }
    }
}