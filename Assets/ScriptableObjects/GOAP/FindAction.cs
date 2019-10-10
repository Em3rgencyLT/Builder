using System;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Find Action")]
    public class FindAction : ActorAction
    {
        public override bool DoAction(GameObject actor)
        {
            throw new NotSupportedException("FindAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(GameObject actor, GameObject target)
        {
            Debug.Log($"{actor.name} is executing FindAction with target {target.name}.");
            return true;
        }
    }
}