using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Find Action")]
    public class FindAction : ActorAction
    {
        public override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("FindAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(Actor actor, GameObject target)
        {
            return false;
        }
    }
}