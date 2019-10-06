using System;
using System.Collections.Generic;

namespace GOAP
{
    public class PlanAction
    {
        private readonly List<State> _requirements;
        private readonly List<State> _effects;
        private readonly Action _resultingAction;

        public PlanAction(List<State> requirements, List<State> effects, Action resultingAction)
        {
            _requirements = requirements;
            _effects = effects;
            _resultingAction = resultingAction;
        }

        public List<State> Requirements => _requirements;

        public List<State> Effects => _effects;

        public Action ResultingAction => _resultingAction;

        private sealed class ActionEqualityComparer : IEqualityComparer<PlanAction>
        {
            public bool Equals(PlanAction x, PlanAction y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x._requirements, y._requirements) && Equals(x._effects, y._effects) && Equals(x._resultingAction, y._resultingAction);
            }

            public int GetHashCode(PlanAction obj)
            {
                unchecked
                {
                    var hashCode = (obj._requirements != null ? obj._requirements.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj._effects != null ? obj._effects.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj._resultingAction != null ? obj._resultingAction.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<PlanAction> ActionComparer { get; } = new ActionEqualityComparer();
    }
}