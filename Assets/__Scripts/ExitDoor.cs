using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private bool isClosed = false;
    private bool isPlayerNear = false;
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerTriggered(collision))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlayerTriggered(collision))
        {
            isPlayerNear = false;
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            if (isClosed && Inventory.instance.HasItem(typeof(ExitKey))) {
                Inventory.instance.UseItem(typeof(ExitKey));
                spriteRenderer.sprite = openedDoor;
                isClosed = false;
            }

            if (!isClosed)
            {
                Debug.Log("Loading new level");
            }
        }
        
    }

    bool isPlayerTriggered(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        return player != null;
    }
}
