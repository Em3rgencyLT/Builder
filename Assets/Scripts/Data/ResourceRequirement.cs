using System;
using Enums;

namespace Data
{
    [Serializable]
    public class ResourceRequirement
    {
        public ResourceType _type;
        public int _amount;

        public ResourceRequirement(ResourceType type, int amount)
        {
            _type = type;
            _amount = amount;
        }

        public ResourceType Type => _type;

        public int Amount => _amount;
    }
}