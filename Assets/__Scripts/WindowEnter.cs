using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowEnter : MonoBehaviour
{
    [SerializeField] WindowExit windowExit;
    [SerializeField] bool isClosed = true;
    [SerializeField] Sprite openedSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Rect Collider")]
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    [SerializeField] Player playerNear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear != null && !isClosed && Input.GetKeyDown(KeyCode.E) && Inventory.instance.HasItem(typeof(Rope)))
        {
            spriteRenderer.sprite = openedWithRopeSprite;
            windowExit.DropRope();
            return;
        }

        if (playerNear != null && isClosed && Input.GetKeyDown(KeyCode.E))
        {
            isClosed = false;
            spriteRenderer.sprite = openedSprite;
        }
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(pointA.position, pointB.position);
        playerNear = null;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            playerNear = collider.GetComponent<Player>();
            if (playerNear != null)
            {
                break;
            }
        }
    }
}
