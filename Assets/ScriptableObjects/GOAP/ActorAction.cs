using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    public abstract class ActorAction : ScriptableObject
    {
        public bool ExecuteAction(Actor actor, GameObject target)
        {
            return target == null ? DoAction(actor) : DoTargetedAction(actor, target);
        }
        protected abstract bool DoAction(Actor actor);
        protected abstract bool DoTargetedAction(Actor actor, GameObject target);
    }
}