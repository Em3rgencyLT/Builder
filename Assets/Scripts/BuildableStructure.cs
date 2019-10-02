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
        [SerializeField] private String menuTitle;
        [SerializeField] private List<ResourceRequirement> resourceRequirements;

        public Sprite MenuSprite => menuSprite;

        public string MenuTitle => menuTitle;

        public List<ResourceRequirement> ResourceRequirements => resourceRequirements;
    }
}