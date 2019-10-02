using System.Collections.Generic;
using Data;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private List<Resource> resources;
        [SerializeField] private Sprite defaultResourceSprite;

        private void Awake()
        {
            AddDefaultResources();
        }

        private void AddDefaultResources()
        {
            var jack = new Resource(ResourceType.Money, defaultResourceSprite, 200);
            var metal = new Resource(ResourceType.Metal, defaultResourceSprite, 10);
            var lumber = new Resource(ResourceType.Lumber, defaultResourceSprite, 10);
            var slag = new Resource(ResourceType.Concrete, defaultResourceSprite, 50);
            
            resources.AddRange(new[]{jack, metal, lumber, slag});
        }

        public List<Resource> Resources => resources;

        public Resource GetResource(ResourceType type)
        {
            return resources.Find(resource => resource.ResourceType == type);
        }

        public int ModifyResourceAmount(ResourceType type, int change)
        {
            var resource = GetResource(type);
            if (resource.Amount + change < 0)
            {
                return resource.Amount;
            }

            if (!resources.Remove(resource))
            {
                return resource.Amount;
            }
            
            var newResource = new Resource(type, resource.Icon, resource.Amount + change);
            resources.Add(newResource);
            return newResource.Amount;
        }

        public bool HasEnoughResource(ResourceType type, int desiredAmount)
        {
            return GetResource(type).Amount >= desiredAmount;
        }
    }
}