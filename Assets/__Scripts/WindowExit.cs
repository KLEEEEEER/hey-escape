using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowExit : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite closedWithRopeSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    [SerializeField] Transform ExitPoint;
    bool isClosed = true;
    public void DropRope()
    {
        spriteRenderer.sprite = closedWithRopeSprite;
        isClosed = false;
    }

    public void OpenWindow()
    {
        spriteRenderer.sprite = openedWithRopeSprite;
    }

    public Vector3 GetExitPointPosition()
    {
        return ExitPoint.position;
    }
}
