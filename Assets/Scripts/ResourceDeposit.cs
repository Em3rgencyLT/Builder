using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Enums;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ResourceDeposit : MonoBehaviour
    {
        [SerializeField] private List<ResourceSource> resourceSources;

        private void Awake()
        {
            if (resourceSources == null)
            {
                resourceSources = new List<ResourceSource>();
            }
        }

        public List<ResourceType> AvailableResources()
        {
            return resourceSources
                .Select(source => source.Resource)
                .Where(resource => resource.Amount > 0)
                .Select(resource => resource.ResourceType)
                .ToList();
        }
    }
}