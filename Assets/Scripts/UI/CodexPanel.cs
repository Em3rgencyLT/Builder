using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        [SerializeField] private TextMeshProUGUI entryTextPrefab;

        private List<CodexEntryButton> _indexButtons;
        const string textVariablePattern = @"{[a-z_0-9]*}";

        private void Start()
        {
            _indexButtons = new List<CodexEntryButton>();
            codexManager.Entries.ForEach(entry =>
            {
                CreateIndexButton(entry, indexPanel.transform, () => UpdateEntryText(entry), true);
            });
        }

        private void CreateIndexButton(CodexEntry entry, Transform parent, Action onClickCallback, bool addToIndexList)
        {
            CodexEntryButton codexEntryButton = Instantiate(entryButtonPrefab, parent);
            codexEntryButton.AssignCodexEntry(entry);
            codexEntryButton.GetComponent<Button>().onClick.AddListener(() => onClickCallback());
            if (addToIndexList)
            {
                _indexButtons.Add(codexEntryButton);
                codexEntryButton.GetComponentInChildren<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            }
        }
        
        private void UpdateEntryText(CodexEntry entry)
        {
            for (int i = 0; i < entryPanel.transform.childCount; i++)
            {
                Destroy(entryPanel.transform.GetChild(i).gameObject);
            }
            CreateRichTextObjects(entry);
        }

        private void CreateRichTextObjects(CodexEntry entry)
        {
            var textParsedToIndex = 0;
            var match = Regex.Match(entry.Content, textVariablePattern);
            if (!match.Success)
            {
                return;
            }
            //create the piece before the first match.
            textParsedToIndex = CreateTextPiece(match, textParsedToIndex, entry);
            while ((match = match.NextMatch()).Success)
            {
                //create the pieces after the first and before last match.
                textParsedToIndex = CreateTextPiece(match, textParsedToIndex, entry);
            }

            if (entry.Content.Length > textParsedToIndex)
            {
                //create the piece after the last match.
                CreateTextPiece(null, textParsedToIndex, entry);
            }
        }

        private int CreateTextPiece(Match match, int textParsedToIndex, CodexEntry entry)
        {
            int textPieceLength;
            if (match == null)
            {
                textPieceLength = entry.Content.Length - textParsedToIndex;
            }
            else
            {
                textPieceLength = match.Index - textParsedToIndex;
            }
             
            var textPiece = Instantiate(entryTextPrefab, entryPanel.transform);
            textPiece.text = entry.Content.Substring(textParsedToIndex, textPieceLength);
            
            if (match == null)
            {
                return entry.Content.Length - 1;
            }
            
            CreateSpecialElementPiece(entry, entry.Content.Substring(match.Index + 1, match.Length - 2));
            return match.Index + match.Length;
        }

        private void CreateSpecialElementPiece(CodexEntry entry, string referenceMatch)
        {
            string[] matchData = referenceMatch.Split('_');
            if (matchData.Length != 2)
            {
                Debug.LogError($"Could not parse reference match {referenceMatch}");
            }
            switch (matchData[0])
            {
                case "ref":
                    Int32.TryParse(matchData[1], out var index);
                    var matchingEntry = entry.References.ElementAtOrDefault(index);
                    if (matchingEntry != null)
                    {
                        CreateIndexButton(matchingEntry, entryPanel.transform, () => SwitchToEntry(matchingEntry), false);
                    }
                    return;
                case "img":
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
    }
}