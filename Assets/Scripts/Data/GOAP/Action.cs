using System.Collections.Generic;
using System.Linq;

namespace Data.GOAP
{
    public class Action
    {
        private readonly List<State> _requirements;
        private readonly List<State> _effects;
        private readonly System.Action _resultingAction;

        public Action(List<State> requirements, List<State> effects, System.Action resultingAction)
        {
            _requirements = requirements;
            _effects = effects;
            _resultingAction = resultingAction;
        }

        public List<State> Requirements => _requirements;

        public List<State> Effects => _effects;

        public System.Action ResultingAction => _resultingAction;

        public List<State> Execute(List<State> worldStates)
        {
            List<State> filtereWorldState = worldStates
                .Where(state =>_requirements
                    .Select(requirement => requirement.Parameter)
                    .ToList()
                    .Contains(state.Parameter))
                .ToList();

            if (filtereWorldState.Count != _requirements.Count)
            {
                return worldStates;
            }
            
            var meetsRequirements = _requirements.All(requirement =>
                worldStates.First(state => state.Parameter == requirement.Parameter).Value == requirement.Value);

            if (!meetsRequirements)
            {
                return worldStates;
            }

            var newStates = new List<State>();

            return newStates;
        }
    }
}