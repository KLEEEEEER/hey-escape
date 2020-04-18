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
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.DisableState);
        SpriteRenderer spriteRenderer = GameManager.instance.PlayerRenderer;
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerComponent.HidePlayer();

        if (spriteRenderer != null) spriteRenderer.enabled = false;

        GameManager.instance.Player.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.Player.position = windowExit.GetExitPointPosition();
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(0.5f);

        windowExit.OpenWindow();

        yield return new WaitForSeconds(0.5f);

        if (spriteRenderer != null) spriteRenderer.enabled = true;
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.IdleState);
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerComponent.UnhidePlayer();
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
