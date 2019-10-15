using System.Collections.Generic;
using Enums;
using Managers;
using ScriptableObjects.GOAP;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace GOAP
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Actor : MonoBehaviour
    {
        [SerializeField] private ActorBehaviourPattern behaviourPattern;
        [SerializeField] private ActorPlanManager actorPlanManager;
        [SerializeField] private GameObject debugTarget;

        private NavMeshAgent _navAgent;
        private List<PlanAction> _plan;

        private void Awake()
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _plan = actorPlanManager.PreparePlan(this);
        }

        private void Update()
        {
            if (_plan != null)
            {
                ExecutePlan();
            }
        }

        public List<GOAPStateValue> CalculateState()
        {
            var state = new List<GOAPStateValue>
            {
                new GOAPStateValue(GOAPState.ActorCanMove, true),
                new GOAPStateValue(GOAPState.ActorIsAlive, true),
                new GOAPStateValue(GOAPState.ActorCanAttack, true),
                new GOAPStateValue(GOAPState.ActorCanHarvest, true)
            };
            return state;
        }

        private void ExecutePlan()
        {
            var jobOngoing = _plan[0].Execute(this, debugTarget);
            if (!jobOngoing)
            {
                _plan.RemoveAt(0);
            }

            if (_plan.Count == 0)
            {
                _plan = null;
            }
        }

        public ActorBehaviourPattern BehaviourPattern => behaviourPattern;

        public NavMeshAgent NavAgent => _navAgent;
    }
}