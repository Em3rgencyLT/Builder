using System;
using Data;
using ScriptableObjects;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(LayoutElement))]
    public class CodexEntryImage : MonoBehaviour
    {
        [SerializeField] private Image imageComponent;
        [SerializeField] private TextMeshProUGUI textComponent;

        public void AssignData(ImageWithNotation imageData)
        {
            imageComponent.sprite = imageData.Image;
            textComponent.text = imageData.Notation;
            GetComponent<LayoutElement>().minHeight = imageData.Image.rect.height;
        }
    }
}