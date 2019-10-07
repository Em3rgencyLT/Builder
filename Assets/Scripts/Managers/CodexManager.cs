using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class CodexManager : MonoBehaviour
    {
        [SerializeField] private List<CodexEntry> entries;

        public List<CodexEntry> Entries => entries;
    }
}