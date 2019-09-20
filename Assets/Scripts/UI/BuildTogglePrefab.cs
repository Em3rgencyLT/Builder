using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class BuildTogglePrefab : MonoBehaviour
    {
        [SerializeField] private BuildableStructure _representedPrefab;

        public BuildableStructure RepresentedPrefab
        {
            get { return _representedPrefab; }
            set { _representedPrefab = value; }
        }
    }
}