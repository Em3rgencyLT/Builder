using UnityEngine;

namespace ScriptableObjects.GOAP
{
    public abstract class ActorAction : ScriptableObject
    {
        public abstract bool DoAction(GameObject actor);
        public abstract bool DoTargetedAction(GameObject actor, GameObject target);
    }
}