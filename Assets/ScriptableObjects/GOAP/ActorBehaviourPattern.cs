using System.Collections.Generic;
using System.Linq;
using GOAP;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Behaviour Pattern")]
    public class ActorBehaviourPattern : ScriptableObject
    {
        [SerializeField] private List<PlanAction> possibleActions;
        [SerializeField] private List<ActorDesire> desires;

        public List<PlanAction> PossibleActions => possibleActions;

        public List<ActorDesire> Desires => desires;

        public List<ActorDesire> SortedDesires()
        {
            return desires.OrderBy(desire => desire.Priority).Reverse().ToList();
        }
    }
}