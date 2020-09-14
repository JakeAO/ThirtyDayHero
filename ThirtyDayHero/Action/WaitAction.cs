using System.Collections.Generic;

namespace ThirtyDayHero
{
    public class WaitAction : IAction
    {
        public class WaitAbility : IAbility
        {
            public static readonly WaitAbility Instance = new WaitAbility();

            public uint Id { get; } = AbilityUtil.NextId;
            public string Name => "Wait";
            public string Desc => "Delay your turn.";
            public uint Speed => 10;
            public IRequirementCalc Requirements => NoRequirements.Instance;
            public ICostCalc Cost => NoCost.Instance;
            public ITargetCalc Target => SelfTargetCalculator.Instance;
            public IEffectCalc Effect => NoEffect.Instance;
        }

        public uint Id { get; } = ActionUtil.NextId;
        public bool Available => true;
        public IAbility Ability => WaitAbility.Instance;
        public ICharacter Source { get; }
        public IReadOnlyCollection<ICharacter> Targets { get; }

        public WaitAction(ICharacter sourceCharacter)
        {
            Source = sourceCharacter;
            Targets = new[] {Source};
        }
    }
}