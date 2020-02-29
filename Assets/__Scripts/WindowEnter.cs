using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WindowEnterState{Closed, Opened, OpenedWithRope}

public class WindowEnter : MonoBehaviour
{
    [SerializeField] WindowExit windowExit;
    [SerializeField] Sprite openedSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Rect Collider")]
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    [SerializeField] Player playerNear;

    [SerializeField] WindowEnterState windowEnterState = WindowEnterState.Closed;

    private void Start()
    {
        switch (windowEnterState)
        {
            case WindowEnterState.Closed:
                break;
            case WindowEnterState.Opened:
                OpenWindow();
                break;
            case WindowEnterState.OpenedWithRope:
                SetRope();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (windowEnterState)
            {
                case WindowEnterState.Closed:
                    OpenWindow();
                    windowEnterState = WindowEnterState.Opened;
                    break;
                case WindowEnterState.Opened:
                    if (Inventory.instance.HasItem(typeof(Rope)))
                    {
                        SetRope();
                        windowEnterState = WindowEnterState.OpenedWithRope;
                    }
                    break;
                case WindowEnterState.OpenedWithRope:
                    StartCoroutine(EnterWindowAnimation());
                    break;
            }
        }
    }

    IEnumerator EnterWindowAnimation()
    {
        SpriteRenderer spriteRenderer = playerNear.gameObject.GetComponent<SpriteRenderer>();
        GameManager.instance.PlayerMovement.disableMovement = true;

        if (spriteRenderer != null) spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.Player.position = windowExit.GetExitPointPosition();

        yield return new WaitForSeconds(0.5f);

        windowExit.OpenWindow();

        yield return new WaitForSeconds(0.5f);

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        GameManager.instance.PlayerMovement.disableMovement = false;
    }

    void OpenWindow()
    {
        spriteRenderer.sprite = openedSprite;
    }

    void SetRope()
    {
        Inventory.instance.UseItem(typeof(Rope));
        spriteRenderer.sprite = openedWithRopeSprite;
        windowExit.DropRope();
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
