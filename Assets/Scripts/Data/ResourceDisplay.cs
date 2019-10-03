using UnityEngine;

namespace Data
{
    public class ResourceDisplay
    {
        public ResourceDisplay(Resource resource, Sprite sprite)
        {
            Resource = resource;
            Sprite = sprite;
        }

        public Resource Resource { get; }

        public Sprite Sprite { get; }
    }
}