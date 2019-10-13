using GOAP;
using UnityEngine;

namespace ScriptableObjects.GOAP
{
    public abstract class ActorAction : ScriptableObject
    {
        public abstract bool DoAction(Actor actor);
        public abstract bool DoTargetedAction(Actor actor, GameObject target);
    }
}