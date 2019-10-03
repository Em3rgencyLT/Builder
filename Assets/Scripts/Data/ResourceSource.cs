using System;
using Enums;

namespace Data
{
    [Serializable]
    public class ResourceSource
    {
        private readonly Resource _resource;
        private readonly int _harvestRate;

        public ResourceSource(Resource resource, int harvestRate)
        {
            _resource = resource;
            _harvestRate = harvestRate;
        }

        public Resource Resource => _resource;

        public int HarvestRate => _harvestRate;

        public int HarvestResult()
        {
            var result = _resource.Amount - _harvestRate;
            if (result < 0)
            {
                result = 0;
            }

            return result;
        }
    }
}