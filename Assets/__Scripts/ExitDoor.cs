using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private bool isClosed = false;

    public void Interact()
    {
        if (isClosed && Inventory.instance.HasItem(typeof(ExitKey)))
        {
            Inventory.instance.UseItem(typeof(ExitKey));
            spriteRenderer.sprite = openedDoor;
            isClosed = false;
            return;
        }

        if (!isClosed)
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
