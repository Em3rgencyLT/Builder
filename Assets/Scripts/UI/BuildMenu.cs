using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform), typeof(ToggleGroup))]
    public class BuildMenu : MonoBehaviour
    {
        [SerializeField] private ObjectPlacementManager _objectPlacementManager;
        [SerializeField] private RectTransform _buildOptionIcon;

        private ToggleGroup _toggleGroup;

        private void Awake()
        {
            if (_objectPlacementManager == null)
            {
                Debug.LogError("BuildMenu is missing ObjectPlacementManager instance!");
            }

            _toggleGroup = GetComponent<ToggleGroup>();
        }

        private void Start()
        {
            _objectPlacementManager.BuildableStructures.ForEach(structure =>
            {
                RectTransform iconPanel = Instantiate(_buildOptionIcon);
                iconPanel.parent = transform;
                Toggle toggle = iconPanel.GetComponentInChildren<Toggle>();
                toggle.group = _toggleGroup;
                toggle.GetComponent<BuildTogglePrefab>().RepresentedPrefab = structure;
                Image image = toggle.GetComponentInChildren<Image>();
                image.sprite = structure.MenuSprite;
                TextMeshProUGUI text = toggle.GetComponentInChildren<TextMeshProUGUI>();
                text.text = structure.MenuTitle;
                toggle.onValueChanged.AddListener(CheckToggledOption);
            });
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
                _objectPlacementManager.BeginObjectPlacement(prefab);
            }
        }
    }
}

