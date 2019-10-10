using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.GOAP;

namespace GOAP.Astar
{
    //TODO: Generalize me!
    public class GOAPPathfinder
    {
        private readonly List<GOAPStateValue> _start;
        private readonly List<GOAPStateValue> _finish;
        private readonly List<PlanAction> _searchSpace;

        private readonly List<GOAPNode> _openList;
        private readonly List<GOAPNode> _closedList;

        private const float UselessStepPenalty = 0.2f;

        public GOAPPathfinder(List<GOAPStateValue> start, List<GOAPStateValue> finish, List<PlanAction> searchSpace)
        {
            _start = start;
            _finish = finish;
            _searchSpace = searchSpace;

            _openList = new List<GOAPNode>();
            _closedList = new List<GOAPNode>();
        }

        public List<PlanAction> FindPath()
        {
            var result = new List<PlanAction>();
            var currentNode = SearchForFinalNode();

            if (currentNode == null)
            {
                return null;
            }

            var parentNode = currentNode.Parent;
            while (parentNode != null)
            {
                result.Add(currentNode.PlanAction);
                currentNode = parentNode;
                parentNode = currentNode.Parent;
            }

            result.Reverse();

            return result;
        }

        private GOAPNode SearchForFinalNode()
        {
            GOAPNode startNode = new GOAPNode(0f, 0f, _start, null, null);
            _openList.Add(startNode);

            while (_openList.Count > 0)
            {
                var bestNode = _openList.Min(node => (node.FValue, node)).Item2;
                if (IsFinishNode(bestNode))
                {
                    return bestNode;
                }
                ProcessNode(bestNode);
            }
            
            return null;
        }
        
        private float CalculateCost(GOAPNode parentNode)
        {
            return parentNode.GValue + 1;
        }

        private float CalculateHeuristic(PlanAction potentialNode)
        {
            float penalty = 0;
            _finish.ForEach(desiredState =>
            {
                GOAPStateValue effect = potentialNode.Effects.FirstOrDefault(state => state.State == desiredState.State);
                if (effect == null)
                {
                    //effect has no bearing on desired state
                    penalty += UselessStepPenalty;
                }else if (effect.Value != desiredState.Value)
                {
                    //effect does opposite of desired state
                    penalty++;
                }
            });
            
            potentialNode.Effects.Where(effect => !_finish.Select(state => state.State).Contains(effect.State)).ToList().ForEach(
                uselessEffect => { penalty += UselessStepPenalty;});

            return penalty;
        }

        private List<GOAPNode> FindNeighbours(GOAPNode node)
        {
            List<GOAPNode> neighbours = new List<GOAPNode>();
            _searchSpace
                .Where(planAction => GOAPStateValue.HaveRequirementsBeenMetByStates(planAction.Requirements, node.AppliedState))
                .ToList()
                .ForEach(planAction =>
                {
                    var newNode = new GOAPNode(
                        CalculateCost(node),
                        CalculateHeuristic(planAction),
                        GOAPStateValue.ApplyEffects(node.AppliedState, planAction.Effects),
                        planAction,
                        node
                    );
                    neighbours.Add(newNode);
                });
            return neighbours;
        }

        private bool IsFinishNode(GOAPNode node)
        {
            return GOAPStateValue.HaveRequirementsBeenMetByStates(_finish, node.AppliedState);
        }

        private void ProcessNode(GOAPNode node)
        {
            List<GOAPNode> neighbours = FindNeighbours(node);
            neighbours
                .Where(neighbour => !_closedList.Contains(neighbour) && !_openList.Contains(neighbour))
                .ToList()
                .ForEach(_openList.Add);
            _closedList.Add(node);
            _openList.Remove(node);
        }
    }
}