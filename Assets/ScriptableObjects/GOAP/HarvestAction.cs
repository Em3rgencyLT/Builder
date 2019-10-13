using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Harvest Action")]
    public class HarvestAction : ActorAction
    {
        private int harvestCount = 3;
        public override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("HarvestAction does not support target-less execution.");
        }

        public override bool DoTargetedAction(Actor actor, GameObject target)
        {
            return false;
        }
    }
}