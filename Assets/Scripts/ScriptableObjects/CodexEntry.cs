using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Codex/Entry")]
    public class CodexEntry : ScriptableObject
    {
        [SerializeField] private string label;
        [SerializeField][TextArea] private string content;
        [SerializeField] private List<Sprite> images;
        [SerializeField] private List<CodexEntry> references;

        public CodexEntry GetReference( string textIndex)
        {
            int.TryParse(textIndex, out var index);
            var matchingEntry = references.ElementAtOrDefault(index);
            if (matchingEntry != null)
            {
                return matchingEntry;
            }

            Debug.LogError($"Could not find reference id: {textIndex} for entry: {label}.");
            return null;
        }
        
        public string Label => label;

        public string Content => content;

        public List<Sprite> Images => images;

        public List<CodexEntry> References => references;
    }
}