using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Attack Action")]
    public class AttackAction : ActorAction
    {
        public override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("AttackAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(Actor actor, GameObject target)
        {
            return false;
        }
    }
}