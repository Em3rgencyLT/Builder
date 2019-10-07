using System.Collections.Generic;
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

        public string Label => label;

        public string Content => content;

        public List<Sprite> Images => images;

        public List<CodexEntry> References => references;
    }
}