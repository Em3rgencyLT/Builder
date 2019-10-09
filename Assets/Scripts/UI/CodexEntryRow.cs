using UnityEngine;
using System.Linq;
using TMPro;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class CodexEntryRow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entryTextPrefab;
        
        public int GetRemainingSpace()
        {
            Canvas.ForceUpdateCanvases();
            var totalWidth = Mathf.FloorToInt(GetComponent<RectTransform>().rect.width);
            var usedWidth = 0;
            for (var i = 0; i < transform.childCount; i++)
            {
                var rectTransform = transform.GetChild(i).GetComponent<RectTransform>();
                usedWidth += rectTransform == null ? 0 : Mathf.FloorToInt(rectTransform.rect.width);
            }
            return totalWidth - usedWidth;
        }

        public int GetRequiredSpace(string text)
        {
            return text.Length * Mathf.FloorToInt(entryTextPrefab.fontSize / 2);
        }
        
        public int GetRequiredSpace(string text, float offset)
        {
            return Mathf.FloorToInt(text.Length * (entryTextPrefab.fontSize / 2) * offset);
        }
    }
}