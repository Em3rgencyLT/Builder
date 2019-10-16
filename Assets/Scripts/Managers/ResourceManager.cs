using System.Collections.Generic;
using System.Linq;
using Data;
using DefaultNamespace;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private List<ResourceDisplay> resourceDisplays;
        [SerializeField] private Sprite defaultResourceSprite;

        private List<ResourceDeposit> _resourceDeposits;

        private void Awake()
        {
            _resourceDeposits = new List<ResourceDeposit>();
            if (resourceDisplays == null)
            {
                resourceDisplays = new List<ResourceDisplay>();
            }
            AddDefaultResources();
        }

        public List<ResourceDisplay> ResourceDisplays => resourceDisplays;

        public Resource GetResource(ResourceType type)
        {
            return resourceDisplays.Select(display => display.Resource).First(resource => resource.ResourceType == type);
        }

        public bool ModifyResourceAmount(ResourceType type, int change)
        {
            var resourceDisplay = GetResourceDisplay(type);
            var resource = resourceDisplay.Resource;
            if (resourceDisplay.Resource.Amount + change < 0)
            {
                return false;
            }

            if (!resourceDisplays.Remove(resourceDisplay))
            {
                return false;
            }
            
            var newResource = new ResourceDisplay(
                new Resource(resource.ResourceType, resource.Amount + change), 
                resourceDisplay.Sprite
            );
            resourceDisplays.Add(newResource);
            return true;
        }

        public void RegisterResourceDeposit(ResourceDeposit deposit)
        {
            _resourceDeposits.Add(deposit);
        }

        public ResourceDeposit FindClosestResource(ResourceType type, Vector3 position)
        {
            return _resourceDeposits
                .Where(deposit => 
                    deposit.AvailableResources().Contains(type))
                .Min(deposit =>(Vector3.Distance(deposit.transform.position, position), deposit).Item2);
        }
        
        private ResourceDisplay GetResourceDisplay(ResourceType type)
        {
            return resourceDisplays.First(resource => resource.Resource.ResourceType == type);
        }
        
        private void AddDefaultResources()
        {
            var jack = new ResourceDisplay(new Resource(ResourceType.Money, 200), defaultResourceSprite);
            var metal = new ResourceDisplay(new Resource(ResourceType.Metal, 10), defaultResourceSprite);
            var lumber = new ResourceDisplay(new Resource(ResourceType.Lumber, 10), defaultResourceSprite);
            var slag = new ResourceDisplay(new Resource(ResourceType.Concrete, 50), defaultResourceSprite);
            
            resourceDisplays.AddRange(new[]{jack, metal, lumber, slag});
        }

        private bool HasEnoughResource(Resource requirement)
        {
            return GetResource(requirement.ResourceType).Amount >= requirement.Amount;
        }

        public bool HasEnoughResources(List<Resource> requirements)
        {
            return requirements.All(HasEnoughResource);
        }
    }
}