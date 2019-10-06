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
    }
}