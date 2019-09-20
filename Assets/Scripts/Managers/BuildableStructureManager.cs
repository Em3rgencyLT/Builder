using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

namespace Managers
{
    public class BuildableStructureManager : MonoBehaviour
    {
        [SerializeField] private List<BuildableStructure> _buildableStructures;

        public List<BuildableStructure> BuildableStructures => _buildableStructures;
    }
}