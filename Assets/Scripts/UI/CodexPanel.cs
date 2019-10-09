using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Data;
using Doozy.Engine.Extensions;
using Enums;
using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CodexPanel : MonoBehaviour
    {
        [SerializeField] private GameObject indexPanel;
        [SerializeField] private GameObject entryPanel;
        [SerializeField] private CodexManager codexManager;
        [SerializeField] private CodexEntryRow codexEntryRowPrefab;
        [SerializeField] private CodexEntryButton entryButtonPrefab;
        [SerializeField] private TextMeshProUGUI entryTextPrefab;

        private List<CodexEntryButton> _indexButtons;
        private CodexEntryRow _currentEntryRow;

        private void Start()
        {
            _indexButtons = new List<CodexEntryButton>();
            codexManager.Entries.ForEach(entry =>
            {
                CreateIndexButton(entry, indexPanel.transform, () => UpdateEntryText(entry), true);
            });
        }
        
        private void UpdateEntryText(CodexEntry entry)
        {
            for (int i = 0; i < entryPanel.transform.childCount; i++)
            {
                Destroy(entryPanel.transform.GetChild(i).gameObject);
            }
            var pieces = entry.Content.Split(' ', '\n').ToList();
            SpawnNewEntryRow();
            pieces.ForEach(piece => CreateTextPiece(entry, piece));
        }

        private void SpawnNewEntryRow()
        {
            _currentEntryRow = Instantiate(codexEntryRowPrefab, entryPanel.transform);
            Canvas.ForceUpdateCanvases();
        }
        
        private void CreateTextPiece(CodexEntry entry, string piece)
        {
            var data = CodexDataBit.FromString(piece);
            switch (data.Type)
            {
                case CodexDataType.Reference:
                    var matchingEntry = entry.GetReference(data.Value);
                    CreateIndexButton(matchingEntry, entryPanel.transform, () => SwitchToEntry(matchingEntry), false);
                    return;
                case CodexDataType.Image:
                    CreateWord("image.jpg");
                    return;
                case CodexDataType.Text:
                    CreateWord(data.Value + "\u00A0");
                    return;
                case CodexDataType.Newline:
                    SpawnNewEntryRow();
                    SpawnNewEntryRow();
                    return;
                default:
                    return;
            }
        }
        
        private void SwitchToEntry(CodexEntry entry)
        {
            var indexButton = _indexButtons.FirstOrDefault(button => button.CodexEntry.Label == entry.Label);
            if (indexButton == null)
            {
                Debug.LogError($"Could not find Codex Entry for {entry.Label}.");
                return;
            }
            
            var buttonComponent = indexButton.GetComponent<Button>();
            buttonComponent.onClick.Invoke();
            buttonComponent.Select();
        }
        
        private void CreateIndexButton(CodexEntry entry, Transform parent, Action onClickCallback, bool addToIndexList)
        {
            CodexEntryButton codexEntryButton = Instantiate(entryButtonPrefab, parent);
            codexEntryButton.AssignCodexEntry(entry);
            codexEntryButton.GetComponent<Button>().onClick.AddListener(() => onClickCallback());
            var textComponent = codexEntryButton.GetComponentInChildren<TextMeshProUGUI>();
            var rectTransform = codexEntryButton.GetComponent<RectTransform>();
            textComponent.alignment = TextAlignmentOptions.Center;
            if (addToIndexList)
            {
                _indexButtons.Add(codexEntryButton);
                rectTransform.sizeDelta = new Vector2(200, 35);
                return;
            }

            var availableSpace = _currentEntryRow.GetRemainingSpace();
            var requiredSpace = _currentEntryRow.GetRequiredSpace(textComponent.text, 1.18f);
            if (requiredSpace > availableSpace)
            {
                SpawnNewEntryRow();
            }
            codexEntryButton.transform.parent = _currentEntryRow.transform;
            LayoutElement layoutElement = codexEntryButton.GetComponent<LayoutElement>();
            layoutElement.minWidth = requiredSpace;
            layoutElement.minHeight = 25;
        }

        private void CreateWord(string input)
        {
            var availableSpace = _currentEntryRow.GetRemainingSpace();
            var requiredSpace = _currentEntryRow.GetRequiredSpace(input);

            if (requiredSpace > availableSpace)
            {
                SpawnNewEntryRow();
            }
            
            var textPiece = Instantiate(entryTextPrefab, _currentEntryRow.transform);
            textPiece.text = input;
            textPiece.rectTransform.rect.AddWidth(requiredSpace).AddHeight(20);
        }
    }
}