using System;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Goto Action")]
    public class GotoAction : ActorAction
    {
        public override bool DoAction(GameObject actor)
        {
            throw new NotSupportedException("GotoAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(GameObject actor, GameObject target)
        {
            Debug.Log($"{actor.name} is executing GotoAction with target {target.name}.");
            return true;
        }
    }
}