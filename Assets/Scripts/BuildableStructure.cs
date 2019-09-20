using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CollisionChecker))]
    public class BuildableStructure : MonoBehaviour
    {
        [SerializeField] private Sprite _menuSprite;
        [SerializeField] private String _menuTitle;

        public Sprite MenuSprite => _menuSprite;

        public string MenuTitle => _menuTitle;
    }
}