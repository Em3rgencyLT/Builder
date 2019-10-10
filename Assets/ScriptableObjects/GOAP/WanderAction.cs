using System;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Wander Action")]
    public class WanderAction : ActorAction
    {
        public override bool DoAction(GameObject actor)
        {
            Debug.Log($"{actor.name} is executing WanderAction.");
            return true;
        }

        public override bool DoTargetedAction(GameObject actor, GameObject target)
        {
            throw new NotSupportedException("WanderAction does not support targeted execution.");
        }
    }
}