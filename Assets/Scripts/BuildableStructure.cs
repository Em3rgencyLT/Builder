using System;
using System.Collections.Generic;
using Data;
using Helpers;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(CollisionChecker))]
    public class BuildableStructure : MonoBehaviour
    {
        [SerializeField] private Sprite menuSprite;
        [SerializeField] private string menuTitle;
        [SerializeField] private List<Resource> resourceRequirements;

        public Sprite MenuSprite => menuSprite;

        public string MenuTitle => menuTitle;

        public List<Resource> ResourceRequirements => resourceRequirements;
    }
}