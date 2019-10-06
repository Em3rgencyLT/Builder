using System.Collections.Generic;

namespace GOAP.Astar
{
    public class GOAPNode
    {
        private readonly PlanAction _planAction;

        public GOAPNode(float g, float h, List<State> appliedState, PlanAction planAction, GOAPNode parent)
        {
            GValue = g;
            HValue = h;
            FValue = g + h;
            AppliedState = appliedState;
            _planAction = planAction;
            Parent = parent;
        }

        public float GValue { get; }

        public float HValue { get; }

        public float FValue { get; }
        
        public List<State> AppliedState { get; }

        public PlanAction PlanAction => _planAction;

        public GOAPNode Parent { get; }

        private sealed class GoapNodeEqualityComparer : IEqualityComparer<GOAPNode>
        {
            public bool Equals(GOAPNode x, GOAPNode y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x._planAction, y._planAction) && x.GValue.Equals(y.GValue) && x.HValue.Equals(y.HValue) && x.FValue.Equals(y.FValue) && Equals(x.AppliedState, y.AppliedState) && Equals(x.Parent, y.Parent);
            }

            public int GetHashCode(GOAPNode obj)
            {
                unchecked
                {
                    var hashCode = (obj._planAction != null ? obj._planAction.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.GValue.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.HValue.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.FValue.GetHashCode();
                    hashCode = (hashCode * 397) ^ (obj.AppliedState != null ? obj.AppliedState.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Parent != null ? obj.Parent.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<GOAPNode> GoapNodeComparer { get; } = new GoapNodeEqualityComparer();
    }
}