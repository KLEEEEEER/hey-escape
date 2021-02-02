using UnityEngine;

namespace HeyEscape.UI
{
    public class CanvasEnabling : MonoBehaviour
    {
        [SerializeField] private Transform CanvasPosition;
        private void OnEnable()
        {
            transform.position = CanvasPosition.position;
        }
    }
}