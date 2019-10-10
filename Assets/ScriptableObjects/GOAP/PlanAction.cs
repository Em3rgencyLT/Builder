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

        public bool Execute(GameObject actor)
        {
            return resultingAction.DoAction(actor);
        }
        
        public bool ExecuteTargeted(GameObject actor, GameObject target)
        {
            return resultingAction.DoTargetedAction(actor, target);
        }

        public List<GOAPStateValue> Requirements => requirements;

        public List<GOAPStateValue> Effects => effects;
    }
}