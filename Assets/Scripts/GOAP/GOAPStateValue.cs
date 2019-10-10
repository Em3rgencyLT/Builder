using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

namespace GOAP
{
    [Serializable]
    public class GOAPStateValue
    {
        [SerializeField] private GOAPState _state;
        [SerializeField] private bool _value;

        public GOAPStateValue(GOAPState state, bool value)
        {
            _state = state;
            _value = value;
        }

        public GOAPState State => _state;

        public bool Value => _value;

        public static List<GOAPStateValue> ApplyEffects(List<GOAPStateValue> initialState, List<GOAPStateValue> effects)
        {
            var newState = new List<GOAPStateValue>();
            effects.ForEach(newState.Add);
            
            initialState
                .Where(state => !effects.Select(effect => effect.State).Contains(state.State))
                .ToList()
                .ForEach(newState.Add);

            return newState;
        }
        
        public static bool HaveRequirementsBeenMetByStates(List<GOAPStateValue> requirements, List<GOAPStateValue> states)
        {
            var filteredWorldStates = states
                .Where(state =>requirements
                    .Select(requirement => requirement.State)
                    .ToList()
                    .Contains(state.State))
                .ToList();

            if (filteredWorldStates.Count != requirements.Count)
            {
                return false;
            }
            
            return requirements.All(requirement =>
                states.First(state => state.State == requirement.State).Value == requirement.Value);
        }
    }
}