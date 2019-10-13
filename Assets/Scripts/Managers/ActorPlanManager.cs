using System.Collections.Generic;
using Enums;
using GOAP;
using GOAP.Astar;
using ScriptableObjects.GOAP;
using UnityEngine;

namespace Managers
{
    public class ActorPlanManager : MonoBehaviour
    {
        private List<GOAPStateValue> CalculateState()
        {
            var state = new List<GOAPStateValue>
            {
                new GOAPStateValue(GOAPState.PlayerHasMetal, false),
                new GOAPStateValue(GOAPState.PlayerHasMoney, false),
                new GOAPStateValue(GOAPState.PlayerHasLumber, false),
                new GOAPStateValue(GOAPState.PlayerHasConcrete, false)
            };
            return state;
        }

        public List<PlanAction> PreparePlan(Actor actor)
        {
            List<GOAPStateValue> combinedState = CalculateState();
            combinedState.AddRange(actor.CalculateState());
            List<ActorDesire> sortedDesires = actor.BehaviourPattern.SortedDesires();

            foreach (var desire in sortedDesires)
            {
                var planner = new GOAPPathfinder(combinedState, desire.DesireList, actor.BehaviourPattern.PossibleActions);
                List<PlanAction> plan = planner.FindPath();
                if (plan != null)
                {
                    return plan;
                }
            }

            return null;
        }
    }
}