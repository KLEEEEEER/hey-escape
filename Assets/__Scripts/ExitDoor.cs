using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private bool isClosed = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClosed;

    public void Interact(PlayerFSM player)
    {
        if (isClosed && Inventory.instance.HasItem(typeof(ExitKey)))
        {
            Inventory.instance.UseItem(typeof(ExitKey));
            spriteRenderer.sprite = openedDoor;
            isClosed = false;
            audioSource.clip = doorOpen;
            audioSource.Play();
            return;
        }
        else
        {
            audioSource.clip = doorClosed;
            audioSource.Play();
        }

        if (!isClosed)
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
