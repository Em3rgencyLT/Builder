using System.Collections.Generic;
using Managers;
using UnityEngine;
using ExtensionMethods;

namespace UI
{
    public class ResourceDisplayPanel : MonoBehaviour
    {
        [SerializeField] private ResourcePanel panelPrefab;
        [SerializeField] private ResourceManager resourceManager;

        private List<ResourcePanel> _resourcePanels;

        private void Awake()
        {
            _resourcePanels = new List<ResourcePanel>();
        }

        private void Start()
        {
            resourceManager.ResourceDisplays.ForEach(display =>
            {
                var panel = Instantiate(panelPrefab, transform, true);
                panel.Image.sprite = display.Sprite;
                panel.Text.text = display.Resource.ResourceType.Label();
                panel.Amount.text = display.Resource.Amount.ToString();
                panel.ResourceType = display.Resource.ResourceType;
                _resourcePanels.Add(panel);
            });
        }

        private void Update()
        {
            UpdateResourceAmounts();
        }

        private void UpdateResourceAmounts()
        {
            _resourcePanels.ForEach(panel =>
            {
                var resource = resourceManager.GetResource(panel.ResourceType);
                panel.Amount.text = resource.Amount.ToString();
            });
        }
    }
}