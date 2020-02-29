using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardInside : MonoBehaviour, IHidePlace, IInteractable
{
    [SerializeField] private bool isOpened = false;
    [SerializeField] private bool isHidden = false;
    public void Hide()
    {
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.enabled = false;
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
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRenderer.enabled = true;
        isHidden = false;
    }
}
