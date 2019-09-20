using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(RectTransform), typeof(ToggleGroup))]
    public class BuildMenu : MonoBehaviour
    {
        [SerializeField] private BuildableStructureManager _buildableStructureManager;
        [SerializeField] private RectTransform _buildOptionIcon;

        private ToggleGroup _toggleGroup;

        private void Awake()
        {
            if (_buildableStructureManager == null)
            {
                Debug.LogError("BuildMenu is missing BuildableStructureManager instance!");
            }

            _toggleGroup = GetComponent<ToggleGroup>();
        }

        private void Start()
        {
            _buildableStructureManager.BuildableStructures.ForEach(structure =>
            {
                RectTransform iconPanel = Instantiate(_buildOptionIcon);
                iconPanel.parent = transform;
                Toggle toggle = iconPanel.GetComponentInChildren<Toggle>();
                toggle.group = _toggleGroup;
                Image image = toggle.GetComponentInChildren<Image>();
                image.sprite = structure.MenuSprite;
                TextMeshProUGUI text = toggle.GetComponentInChildren<TextMeshProUGUI>();
                text.text = structure.MenuTitle;
            });
        }
    }
}

