using System;
using System.Collections.Generic;
using ScriptableObjects.GOAP;

namespace GOAP.Astar
{
    public class GOAPNode : IComparable
    {
        private readonly PlanAction _planAction;

        public GOAPNode(float g, float h, List<GOAPStateValue> appliedState, PlanAction planAction, GOAPNode parent)
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
        
        public List<GOAPStateValue> AppliedState { get; }

        public PlanAction PlanAction => _planAction;

        public GOAPNode Parent { get; }
        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case null:
                    return 1;
                case GOAPNode otherNode:
                    return FValue.CompareTo(otherNode.FValue);
                default:
                    throw new ArgumentException("Object is not a GOAPNode");
            }
        }
    }
}