using System;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Harvest Action")]
    public class HarvestAction : ActorAction
    {
        public override bool DoAction(GameObject actor)
        {
            throw new NotSupportedException("HarvestAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(GameObject actor, GameObject target)
        {
            Debug.Log($"{actor.name} is executing HarvestAction with target {target.name}.");
            return true;
        }
    }
}