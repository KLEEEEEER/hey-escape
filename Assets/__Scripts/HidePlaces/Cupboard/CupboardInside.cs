using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardInside : MonoBehaviour, IHidePlace, IInteractable
{
    [SerializeField] private bool isOpened = false;
    [SerializeField] private bool isHidden = false;

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
        if (isHidden && GameManager.instance.Player.gameObject != null && GameManager.instance.Player.gameObject.transform.position != transform.position)
        {
            GameManager.instance.Player.gameObject.transform.position = transform.position;
        }
    }
    public void Hide()
    {
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.DisableState);
        GameManager.instance.PlayerComponent.HidePlayer();
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.enabled = false;
        spriteRenderer.sprite = openedWithPlayer;
        isHidden = true;
    }

    public void Interact()
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

        if (!isHidden)
        {
            Hide();
        }
        else
        {
            Unhide();
        }
    }

    public void Unhide()
    {
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.IdleState);
        GameManager.instance.PlayerComponent.UnhidePlayer();
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRenderer.enabled = true;
        spriteRenderer.sprite = opened;
        isHidden = false;
    }
}
