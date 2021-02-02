using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.HidePlaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        /*if (isHidden && GameManager.instance.Player.gameObject != null && GameManager.instance.Player.gameObject.transform.position != transform.position)
        {
            GameManager.instance.Player.gameObject.transform.position = transform.position;
        }*/
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
