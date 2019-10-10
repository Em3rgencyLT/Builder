using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class CodexManager : MonoBehaviour
    {
        [SerializeField] private List<CodexEntry> entries;

        public List<CodexEntry> Entries => entries;

        private void Awake()
        {
            entries = entries.OrderBy(entry => entry.Label).ToList();
        }
    }
}