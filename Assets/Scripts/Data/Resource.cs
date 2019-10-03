using Enums;

namespace Data
{   
    public class Resource
    {
        public Resource(ResourceType resourceType, int amount = 0)
        {
            ResourceType = resourceType;
            if (amount < 0)
            {
                amount = 0;
            }
            Amount = amount;
        }

        public ResourceType ResourceType { get; }

        public int Amount { get; }
    }
}