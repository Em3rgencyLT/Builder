using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform), typeof(ToggleGroup))]
    public class BuildMenu : MonoBehaviour
    {
        [SerializeField] private ObjectPlacementManager objectPlacementManager;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private RectTransform buildOptionIcon;

        private ToggleGroup _toggleGroup;
        private List<Toggle> _toggles;

        private void Awake()
        {
            if (objectPlacementManager == null)
            {
                Debug.LogError("BuildMenu is missing ObjectPlacementManager instance!");
            }

            _toggleGroup = GetComponent<ToggleGroup>();
        }

        private void Start()
        {
            _toggles = new List<Toggle>();
            objectPlacementManager.BuildableStructures.ForEach(structure =>
            {
                RectTransform iconPanel = Instantiate(buildOptionIcon, transform, true);
                Toggle toggle = iconPanel.GetComponentInChildren<Toggle>();
                _toggles.Add(toggle);
                toggle.group = _toggleGroup;
                toggle.GetComponent<BuildTogglePrefab>().RepresentedPrefab = structure;
                Image image = toggle.GetComponentInChildren<Image>();
                image.sprite = structure.MenuSprite;
                TextMeshProUGUI text = toggle.GetComponentInChildren<TextMeshProUGUI>();
                text.text = structure.MenuTitle;
                toggle.onValueChanged.AddListener(CheckToggledOption);
            });
        }

        private void Update()
        {
            CheckToggleInteractability();
        }

        public void ClearSelection()
        {
            _toggleGroup.SetAllTogglesOff();
        }

        private void CheckToggledOption(bool value)
        {
            if (value)
            {
                BuildableStructure prefab = _toggleGroup.ActiveToggles().First().GetComponent<BuildTogglePrefab>()
                    .RepresentedPrefab;
                objectPlacementManager.BeginObjectPlacement(prefab);
            }
        }

        private void CheckToggleInteractability()
        {
            _toggles.ForEach(toggle =>
            {
                var buildableStructure = toggle.GetComponent<BuildTogglePrefab>().RepresentedPrefab;
                var hasEnoughResources = resourceManager.HasEnoughResources(buildableStructure.ResourceRequirements);
                if (toggle.interactable && !hasEnoughResources)
                {
                    toggle.interactable = false;
                } else if (!toggle.interactable && hasEnoughResources)
                {
                    toggle.interactable = true;
                }
            });
        }
    }
}

