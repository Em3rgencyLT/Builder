using System;
using ScriptableObjects;
using ScriptableObjects.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class CodexEntryButton : MonoBehaviour
    {
        [SerializeField] private CodexEntry codexEntry;

        public void AssignCodexEntry(CodexEntry entry, Action onClickCallback)
        {
            codexEntry = entry;
            var text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = entry.Label;
            GetComponent<Button>().onClick.AddListener(() => onClickCallback());
        }

        public CodexEntry CodexEntry => codexEntry;
    }
}