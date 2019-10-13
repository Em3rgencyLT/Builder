using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Wander Action")]
    public class WanderAction : ActorAction
    {
        public override bool DoAction(Actor actor)
        {
            return false;
        }

        public override bool DoTargetedAction(Actor actor, GameObject target)
        {
            throw new NotSupportedException("WanderAction does not support targeted execution.");
        }
    }
}