using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace ScriptableObjects.UI
{
    [CreateAssetMenu(menuName = "Codex/Entry")]
    public class CodexEntry : ScriptableObject
    {
        [SerializeField] private string label;
        [SerializeField] [TextArea] private string content;
        [SerializeField] private List<ImageWithNotation> images;
        [SerializeField] private List<CodexEntry> references;

        public CodexEntry GetReference(string textIndex)
        {
            int.TryParse(textIndex, out var index);
            var matchingEntry = references.ElementAtOrDefault(index);
            if (matchingEntry != null)
            {
                return matchingEntry;
            }

            Debug.LogError($"Could not find Reference of id: {textIndex} for entry: {label}.");
            return null;
        }

        public ImageWithNotation GetImage(string textIndex)
        {
            int.TryParse(textIndex, out var index);
            var matchingEntry = images.ElementAtOrDefault(index);
            if (matchingEntry != null)
            {
                return matchingEntry;
            }
            
            Debug.LogError($"Could not find Image of id: {textIndex} for entry: {label}.");
            return null;
        }

        public string Label => label;

        public string Content => content;

        public List<ImageWithNotation> Images => images;

        public List<CodexEntry> References => references;
    }
}