using System.Collections.Generic;
using System.Linq;
using Data;
using Enums;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourceDeposit : MonoBehaviour
    {
        [SerializeField] private List<ResourceSource> _resourceSources;
        
        public List<ResourceType> AvailableResources()
        {
            return _resourceSources
                .Select(source => source.Resource)
                .Where(resource => resource.Amount > 0)
                .Select(resource => resource.ResourceType)
                .ToList();
        }
    }
}