using System;
using System.Text;
using ThirtyDayHero;

namespace ConsoleApp
{
    public class ConsolePlayer
    {
        private CombatManager _combatManager = null;

        public ConsolePlayer()
        {
            _combatManager = CombatManagerUtil.CreateDebugLoadedManager(new ConsoleCharacterController());
            _combatManager.GameStateUpdate += OnGameStateUpdate;
            _combatManager.ActionTaken += OnActionTaken;
            _combatManager.CombatComplete += OnCombatComplete;
            _combatManager.Start();
        }

        private void OnGameStateUpdate(IGameState gameState)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"[GameState {gameState.Id} ================================ {gameState.State}]");
            foreach (IInitiativePair initPair in gameState.InitiativeOrder)
            {
                IInitiativeActor actor = initPair.Entity;
                ICharacterActor cActor = actor as ICharacterActor;

                // Line 1: [id] name (P: party, I: init) (C: class)
                if (!actor.Alive)
                    sb.Append("-DEAD- ");
                sb.Append($"[{actor.Id}] {actor.Name} (P: {actor.Party}, I: {initPair.Initiative} - {actor.Initiative})");
                if (actor is IPlayerCharacterActor playerCharacterActor)
                    sb.Append($" (C: {playerCharacterActor.Class.Name})");
                if (cActor != null)
                    sb.Append($" (LVL: {cActor.Stats.GetStat(StatType.LVL)}, EXP: {cActor.Stats.GetStat(StatType.EXP)})");
                sb.AppendLine();

                // Line 2:      [HP: xx/MAX][STA: xx/MAX][STR: xx][DEX: xx][CON: xx][INT: xx][MAG: xx][CHA: xx]
                if (cActor != null)
                {
                    sb.Append("     ");
                    sb.Append($"[HP: {cActor.Stats.GetStat(StatType.HP)}/{cActor.Stats.GetStat(StatType.HP_Max)}]");
                    sb.Append($"[STA: {cActor.Stats.GetStat(StatType.STA)}/{cActor.Stats.GetStat(StatType.STA_Max)}]");
                    sb.Append($"[STR: {cActor.Stats.GetStat(StatType.STR)}]");
                    sb.Append($"[DEX: {cActor.Stats.GetStat(StatType.DEX)}]");
                    sb.Append($"[CON: {cActor.Stats.GetStat(StatType.CON)}]");
                    sb.Append($"[INT: {cActor.Stats.GetStat(StatType.INT)}]");
                    sb.Append($"[MAG: {cActor.Stats.GetStat(StatType.MAG)}]");
                    sb.Append($"[CHA: {cActor.Stats.GetStat(StatType.CHA)}]");
                    sb.AppendLine();
                }
            }
            sb.AppendLine("[======================================================]");
            
            if (gameState.ActionPending)
            {
                Console.WriteLine(sb);
            }
            else
            {
                sb.AppendLine("Press any key to continue...");
                Console.WriteLine(sb);
                if (Console.Read() != 0)
                {
                    _combatManager.Continue();
                }
            }
        }

        private void OnActionTaken(IAction action)
        {
            IAbility ability = action.Ability;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Action Taken -----------------]");
            sb.AppendLine($"    [{action.Id}] {ability.Name} ({ability.Id})");
            sb.AppendLine($"    [Source] {action.Source.Name} ({action.Source.Id})");
            foreach (ICharacterActor target in action.Targets)
            {
                sb.AppendLine($"    [Target] {target.Name} ({target.Id})");
            }
            sb.AppendLine("[------------------------------]");
            Console.WriteLine(sb);
        }

        private void OnCombatComplete(uint winningParty)
        {
            Console.WriteLine($"!!! Winning Party: {winningParty} !!!");
        }
    }
}