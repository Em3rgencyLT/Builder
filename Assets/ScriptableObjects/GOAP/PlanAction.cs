using System.Collections.Generic;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Plan Action")]
    public class PlanAction : ScriptableObject
    {
        [SerializeField] private List<GOAPStateValue> requirements;
        [SerializeField] private List<GOAPStateValue> effects;
        [SerializeField] private ActorAction resultingAction;

        public bool Execute(Actor actor, GameObject target)
        {
            Debug.Log($"{actor.name} is executing {name} with target {target}.");
            return resultingAction.ExecuteAction(actor, target);
        }

        public List<GOAPStateValue> Requirements => requirements;

        public List<GOAPStateValue> Effects => effects;
    }
}