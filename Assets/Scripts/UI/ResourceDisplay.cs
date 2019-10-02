using System.Collections.Generic;
using Managers;
using UnityEngine;
using ExtensionMethods;

namespace UI
{
    public class ResourceDisplay : MonoBehaviour
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
            resourceManager.Resources.ForEach(resource =>
            {
                var panel = Instantiate(panelPrefab, transform, true);
                panel.Image.sprite = resource.Icon;
                panel.Text.text = resource.ResourceType.Label();
                panel.Amount.text = resource.Amount.ToString();
                panel.ResourceType = resource.ResourceType;
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