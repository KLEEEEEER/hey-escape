using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class WindowEnter : MonoBehaviour, IInteractable
{
    [SerializeField] WindowExit windowExit;
    [SerializeField] WindowExit[] windowsExitToDropRope;
    [SerializeField] Sprite openedSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    [SerializeField] Sprite openedWithRopeAndPlayerSprite;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Transform ExitPoint;
    [SerializeField] AudioSource openingSound;

    private WaitForSeconds delay = new WaitForSeconds(0.5f);

    //[SerializeField] Player playerNear;
    enum WindowEnterState { Closed, Opened, OpenedWithRope, PlayerInside }

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

    IEnumerator EnterWindowAnimation(PlayerFSM player)
    {
        player.TransitionToState(player.DisableState);
        player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Hidden);
        player.PlayerMovement.SetEnabled(true);

        if (player.Renderer != null) player.Renderer.enabled = false;

        player.transform.position = transform.position;
        player.Rigidbody2D.velocity = new Vector2(0, 0);

        yield return delay;

        player.transform.position = windowExit.GetExitPointPosition();
        player.Rigidbody2D.velocity = new Vector2(0, 0);

        yield return delay;

        windowExit.OpenWindow();

        yield return delay;

        player.PlayerMovement.SetEnabled(false);
        windowExit.PlayerWait(player);
    }

    void OpenWindow()
    {
        spriteRenderer.sprite = openedSprite;
        openingSound.Play();
    }

    void SetRope(PlayerFSM player = null)
    {
        if (player != null)
        {
            player.Inventory.UseItem(typeof(Rope));
        }

        spriteRenderer.sprite = openedWithRopeSprite;
        windowExit.DropRope();
        if (windowsExitToDropRope.Length > 0)
        {
            foreach (WindowExit window in windowsExitToDropRope)
            {
                window.DropRope();
            }
        }
    }

    public void Interact(PlayerFSM player)
    {
        switch (windowEnterState)
        {
            case WindowEnterState.Closed:
                OpenWindow();
                windowEnterState = WindowEnterState.Opened;
                break;
            case WindowEnterState.Opened:
                if (player.Inventory.HasItem(typeof(Rope)) && windowExit != null)
                {
                    SetRope(player);
                    windowEnterState = WindowEnterState.OpenedWithRope;
                }
                break;
            case WindowEnterState.PlayerInside:
                break;
            case WindowEnterState.OpenedWithRope:
                if (windowExit != null)
                    StartCoroutine(EnterWindowAnimation(player));
                break;
        }
    }

    public void PlayerWait(PlayerFSM player)
    {
        player.TransitionToState(player.InWindowState);
        spriteRenderer.sprite = openedWithRopeAndPlayerSprite;
        windowEnterState = WindowEnterState.PlayerInside;
        player.InWindowState.OnWindowExit.AddListener(ExitWindow);
    }

    public void ExitWindow(PlayerFSM player)
    {
        windowEnterState = WindowEnterState.OpenedWithRope;
        spriteRenderer.sprite = openedWithRopeSprite;
        player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Visible);
    }

    public Vector3 GetExitPointPosition() => ExitPoint.position;
}
