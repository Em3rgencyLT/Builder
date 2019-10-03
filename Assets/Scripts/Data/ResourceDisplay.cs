using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class ResourceDisplay
    {
        [SerializeField] private Resource resource;
        [SerializeField] private Sprite sprite;
        
        public ResourceDisplay(Resource resource, Sprite sprite)
        {
            this.resource = resource;
            this.sprite = sprite;
        }

        public Resource Resource => resource;

        public Sprite Sprite => sprite;
    }
}