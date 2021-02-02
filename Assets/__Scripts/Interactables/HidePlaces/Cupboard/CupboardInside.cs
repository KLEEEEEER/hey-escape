using HeyEscape.Core.Interfaces;
using HeyEscape.Core.Inventory;
using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.GameItems;
using UnityEngine;

namespace HeyEscape.Interactables.HidePlaces
{
    public class CupboardInside : MonoBehaviour, IHidePlace, IInteractable
    {
        [SerializeField] private bool isOpened = false;
        [SerializeField] private bool isHidden = false;
        [SerializeField] private HidePlaceInfoSO hidePlaceInfo;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite closed;
        [SerializeField] private Sprite opened;
        [SerializeField] private Sprite openedWithPlayer;

        private void Start()
        {
            spriteRenderer.sprite = (isOpened) ? opened : closed;
        }

        public void Interact(PlayerFSM player)
        {
            if (!isOpened)
            {
                if (Inventory.instance.HasItem(typeof(CupboardKey)))
                {
                    Inventory.instance.UseItem(typeof(CupboardKey));
                    isOpened = true;
                    spriteRenderer.sprite = opened;
                }
                return;
            }
        }

        public bool IsAccessible()
        {
            return isOpened || Inventory.instance.HasItem(typeof(CupboardKey));
        }

        public HidePlaceInfoSO GetHidePlaceInfo()
        {
            hidePlaceInfo.transform = transform.position;
            return hidePlaceInfo;
        }

        public void OnHide(PlayerFSM player)
        {
        }

        public void OnUnhide(PlayerFSM player)
        {
        }
    }
}