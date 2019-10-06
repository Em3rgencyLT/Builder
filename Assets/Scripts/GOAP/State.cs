using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GOAP
{
    [Serializable]
    public class State
    {
        [SerializeField] private readonly string _name;
        [SerializeField] private readonly bool _value;

        public State(string name, bool value)
        {
            _name = name;
            _value = value;
        }

        public string Name => _name;

        public bool Value => _value;

        public static List<State> ApplyEffecs(List<State> initialState, List<State> effects)
        {
            List<State> newState = new List<State>();
            effects.ForEach(newState.Add);
            
            initialState
                .Where(state => !effects.Select(effect => effect.Name).Contains(state.Name))
                .ToList()
                .ForEach(newState.Add);

            return newState;
        }
        
        public static bool HaveRequirementsBeenMetByStates(List<State> requirements, List<State> states)
        {
            List<State> filteredWorldStates = states
                .Where(state =>requirements
                    .Select(requirement => requirement.Name)
                    .ToList()
                    .Contains(state.Name))
                .ToList();

            if (filteredWorldStates.Count != requirements.Count)
            {
                return false;
            }
            
            return requirements.All(requirement =>
                states.First(state => state.Name == requirement.Name).Value == requirement.Value);
        }

        private sealed class NameValueEqualityComparer : IEqualityComparer<State>
        {
            public bool Equals(State x, State y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x._name, y._name) && x._value == y._value;
            }

            public int GetHashCode(State obj)
            {
                unchecked
                {
                    return ((obj._name != null ? obj._name.GetHashCode() : 0) * 397) ^ obj._value.GetHashCode();
                }
            }
        }

        public static IEqualityComparer<State> NameValueComparer { get; } = new NameValueEqualityComparer();
    }
}