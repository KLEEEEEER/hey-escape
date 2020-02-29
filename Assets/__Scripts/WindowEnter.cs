using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WindowEnterState{Closed, Opened, OpenedWithRope}

public class WindowEnter : MonoBehaviour, IInteractable
{
    [SerializeField] WindowExit windowExit;
    [SerializeField] Sprite openedSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    [SerializeField] SpriteRenderer spriteRenderer;

    //[SerializeField] Player playerNear;

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

    IEnumerator EnterWindowAnimation()
    {
        SpriteRenderer spriteRenderer = GameManager.instance.PlayerRenderer;
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

    public void Interact()
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
