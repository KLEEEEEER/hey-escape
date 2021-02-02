using HeyEscape.Core.Player.FSM;
using UnityEngine;

namespace HeyEscape.Interactables.HidePlaces
{
    public class Door : MonoBehaviour, IHidePlace
    {
        [SerializeField] private Vector3 PlayerHideScale = new Vector3(0.9f, 0.9f, 0.9f);
        [SerializeField] private Color HideColor;
        [SerializeField] private float smoothTime;

        private bool isHidden = false;
        private Vector3 currentVelocity;

        public bool IsAccessible()
        {
            return true;
        }

        [SerializeField] private HidePlaceInfoSO hidePlaceInfoSO;
        public HidePlaceInfoSO GetHidePlaceInfo()
        {
            hidePlaceInfoSO.transform = transform.position;
            return hidePlaceInfoSO;
        }

        public void OnHide(PlayerFSM player)
        {
        }

        public void OnUnhide(PlayerFSM player)
        {
        }
    }
}