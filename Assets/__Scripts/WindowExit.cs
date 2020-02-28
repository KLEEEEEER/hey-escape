using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowExit : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite closedWithRopeSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    bool isClosed = true;
    public void DropRope()
    {
        spriteRenderer.sprite = closedWithRopeSprite;
        isClosed = false;
    }
}
