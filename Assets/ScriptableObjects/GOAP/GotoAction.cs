using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Goto Action")]
    public class GotoAction : ActorAction
    {
        public override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("GotoAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(Actor actor, GameObject target)
        {
            return false;
        }
    }
}