using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class CodexEntryButton : MonoBehaviour
    {
        [SerializeField] private CodexEntry codexEntry;

        public void AssignCodexEntry(CodexEntry entry)
        {
            codexEntry = entry;
            var text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = entry.Label;
            //TODO: attach onclick event here
        }

        public CodexEntry CodexEntry => codexEntry;
    }
}