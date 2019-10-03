using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{   
    [Serializable]
    public class Resource
    {
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int amount;
        
        public Resource(ResourceType resourceType, int amount = 0)
        {
            this.resourceType = resourceType;
            if (amount < 0)
            {
                amount = 0;
            }
            this.amount = amount;
        }

        public ResourceType ResourceType => resourceType;

        public int Amount => amount;
    }
}