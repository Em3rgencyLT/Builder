using Managers;
using UnityEngine;

namespace UI
{
    public class CodexPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _indexPanel;
        [SerializeField] private GameObject _entryPanel;
        [SerializeField] private CodexManager _codexManager;
        [SerializeField] private CodexEntryButton _entryButtonPrefab;

        private void Start()
        {
            _codexManager.Entries.ForEach(entry =>
            {
                CodexEntryButton codexEntryButton = Instantiate(_entryButtonPrefab, _indexPanel.transform);
                codexEntryButton.AssignCodexEntry(entry);
            });
        }
    }
}