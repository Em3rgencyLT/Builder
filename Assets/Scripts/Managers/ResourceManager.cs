using System.Collections.Generic;
using Data;
using Enums;
using UnityEngine;

namespace Managers
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private List<Resource> _resources;
        [SerializeField] private Sprite defaultResourceSprite;

        private void Start()
        {
            AddDefaultResources();
        }

        private void AddDefaultResources()
        {
            var jack = new Resource(ResourceType.Money, defaultResourceSprite, 200);
            var metal = new Resource(ResourceType.Metal, defaultResourceSprite, 10);
            var lumber = new Resource(ResourceType.Lumber, defaultResourceSprite, 10);
            var slag = new Resource(ResourceType.Concrete, defaultResourceSprite, 50);
            
            _resources.AddRange(new[]{jack, metal, lumber, slag});
        }
    }
}