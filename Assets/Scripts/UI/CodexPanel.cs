using System;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private CodexEntryButton entryButtonPrefab;

        private List<CodexEntryButton> _indexButtons;

        private void Start()
        {
            _indexButtons = new List<CodexEntryButton>();
            codexManager.Entries.ForEach(entry =>
            {
                CodexEntryButton codexEntryButton = Instantiate(entryButtonPrefab, indexPanel.transform);
                codexEntryButton.AssignCodexEntry(entry);
                codexEntryButton.GetComponent<Button>().onClick.AddListener(() => UpdateEntryText(entry));
                _indexButtons.Add(codexEntryButton);
            });
        }
        
        private void UpdateEntryText(CodexEntry entry)
        {
            entryPanel.GetComponentInChildren<TextMeshProUGUI>().text = entry.Content;
        }

        private void SwitchToEntry(CodexEntry entry)
        {
            var indexButton = _indexButtons.FirstOrDefault(button => button.CodexEntry.Label == entry.Label);
            if (indexButton == null)
            {
                Debug.LogError($"Could not find Codex Entry for {entry.Label}.");
                return;
            }
            
            indexButton.GetComponent<Button>().onClick.Invoke();
        }
    }
}