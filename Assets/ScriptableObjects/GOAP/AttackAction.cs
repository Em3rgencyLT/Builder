using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Attack Action")]
    public class AttackAction : ActorAction
    {   
        protected override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("AttackAction does not support target-less execution.");
        }

        protected override bool DoTargetedAction(Actor actor, GameObject target)
        {
            return false;
        }
    }
}