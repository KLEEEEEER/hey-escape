using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Text itemText;

        public void SetInfo(Sprite sprite, string text)
        {
            image.sprite = sprite;
            itemText.text = text;
        }

    }
}