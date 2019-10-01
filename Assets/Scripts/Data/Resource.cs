using System;
using Enums;
using UnityEngine;

namespace Data
{   
    //TODO: remove serializable and public fields, they are only used to debug in inspector
    [Serializable]
    public class Resource
    {
        public ResourceType _resourceType;
        public Sprite _icon;
        public int _amount;

        public Resource(ResourceType resourceType, Sprite icon, int amount = 0)
        {
            _resourceType = resourceType;
            _icon = icon;
            _amount = amount;
        }

        public ResourceType ResourceType => _resourceType;

        public Sprite Icon => _icon;

        public int Amount => _amount;
    }
}