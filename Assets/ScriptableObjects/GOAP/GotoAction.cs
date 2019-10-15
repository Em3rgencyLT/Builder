using System;
using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Goto Action")]
    public class GotoAction : ActorAction
    {
        private const float StopDistance = 2f;
        protected override bool DoAction(Actor actor)
        {
            throw new NotSupportedException("GotoAction does not support target-less execution.");
        }

        protected override bool DoTargetedAction(Actor actor, GameObject target)
        {
            var agent = actor.NavAgent;
            agent.stoppingDistance = StopDistance;
            
            if (agent.hasPath && agent.remainingDistance < StopDistance || agent.isPathStale)
            {
                agent.ResetPath();
                return false;
            }
            
            agent.destination = target.transform.position;

            return true;
        }
    }
}