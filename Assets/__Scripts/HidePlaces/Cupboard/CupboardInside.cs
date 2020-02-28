using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardInside : MonoBehaviour, IHidePlace
{
    [SerializeField] private bool isOpened = false;
    public void Hide(GameObject player)
    {
        if (!isOpened) return;
        player.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.enabled = false;
    }

    public bool IsAccessible()
    {
        if (Inventory.instance.HasItem(typeof(CupboardKey)))
        {
            Inventory.instance.UseItem(typeof(CupboardKey));
            isOpened = true;
        }

        return isOpened;
    }

    public void Unhide(GameObject player)
    {
        player.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRenderer.enabled = true;
    }
}
