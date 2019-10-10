using System;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Attack Action")]
    public class AttackAction : ActorAction
    {
        public override bool DoAction(GameObject actor)
        {
            throw new NotSupportedException("AttackAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(GameObject actor, GameObject target)
        {
            Debug.Log($"{actor.name} is executing AttackAction with target {target.name}.");
            return true;
        }
    }
}