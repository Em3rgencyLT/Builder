using Enums;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourcePanel : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI amount;

        public ResourceType ResourceType { get; set; }

        public Image Image => image;

        public TextMeshProUGUI Text => text;

        public TextMeshProUGUI Amount => amount;
    }
}