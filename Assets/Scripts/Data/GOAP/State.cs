using System;
using UnityEngine;

namespace Data.GOAP
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
    }
}