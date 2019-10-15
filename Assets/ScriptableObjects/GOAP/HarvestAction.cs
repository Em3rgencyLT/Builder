using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Harvest Action")]
    public class HarvestAction : ActorAction
    {
        private int harvestCount = 3;
        protected override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("HarvestAction does not support target-less execution.");
        }

        protected override bool DoTargetedAction(Actor actor, GameObject target)
        {
            return false;
        }
    }
}