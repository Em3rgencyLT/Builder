using System.Collections.Generic;
using GOAP;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.GOAP
{
    [CreateAssetMenu(menuName = "GOAP/Actor/Desire")]
    public class ActorDesire : ScriptableObject
    {
        [SerializeField] private List<GOAPStateValue> desireList;
        [SerializeField] private int priority;

        public List<GOAPStateValue> DesireList => desireList;

        public int Priority => priority;
    }
}