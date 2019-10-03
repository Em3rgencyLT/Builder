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

        public List<State> GetStatesAfterAction(List<State> initialStates)
        {
            var newStates = new List<State>();
            _effects.ForEach(effect => newStates.Add(effect));
            initialStates
                .Where(state => !_effects.Select(effect => effect.Name).Contains(state.Name))
                .ToList()
                .ForEach(state => newStates.Add(state));
            
            return newStates;
        }

        public List<State> Execute(List<State> worldStates)
        {
            if (!HasItsRequirementsMet(worldStates))
            {
                return worldStates;
            }
            
            _resultingAction();
            return GetStatesAfterAction(worldStates);
        }
        
        private bool HasItsRequirementsMet(List<State> worldStates)
        {
            List<State> filteredWorldStates = worldStates
                .Where(state =>_requirements
                    .Select(requirement => requirement.Name)
                    .ToList()
                    .Contains(state.Name))
                .ToList();

            if (filteredWorldStates.Count != _requirements.Count)
            {
                return false;
            }
            
            return _requirements.All(requirement =>
                worldStates.First(state => state.Name == requirement.Name).Value == requirement.Value);
        }
    }
}