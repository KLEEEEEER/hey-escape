using Cinemachine;
using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowExit : MonoBehaviour, IInteractable
{
    [SerializeField] WindowEnter windowEnter;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite closedWithRopeSprite;
    [SerializeField] Sprite openedWithRopeSprite;
    [SerializeField] Sprite openedWithRopeWithPlayerSprite;
    [SerializeField] Transform ExitPoint;
    [SerializeField] AudioSource openingSound;
    bool isClosed = true;
    WindowExitState currentWindowExitState = WindowExitState.Closed;

    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private WaitForSeconds delay = new WaitForSeconds(0.5f);
    enum WindowExitState { Closed, OpenedWithRope, PlayerInside }
    public void DropRope()
    {
        spriteRenderer.sprite = closedWithRopeSprite;
    }

    public void OpenWindow()
    {
        if (isClosed)
        {
            openingSound.Play();
            spriteRenderer.sprite = openedWithRopeSprite;
            currentWindowExitState = WindowExitState.OpenedWithRope;
            isClosed = false;
        }
    }

    public void PlayerWait(PlayerFSM player)
    {
        player.TransitionToState(player.InWindowState);
        player.InWindowState.OnWindowExit.AddListener(ExitWindow);
        spriteRenderer.sprite = openedWithRopeWithPlayerSprite;
        currentWindowExitState = WindowExitState.PlayerInside;
    }

    public Vector3 GetExitPointPosition()
    {
        return ExitPoint.position;
    }

    IEnumerator EnterWindowAnimation(PlayerFSM player)
    {
        SetVirtualCameraPriority(11);
        windowEnter.SetVirtualCameraPriority(12);

        player.TransitionToState(player.DisableState);
        player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Hidden);
        player.PlayerMovement.SetEnabled(true);
        windowEnter.SetEnabledVirtualCamera(true);
        player.VirtualCamera.gameObject.SetActive(false);

        if (player.Renderer != null) player.Renderer.enabled = false;

        player.transform.position = transform.position;
        player.Rigidbody2D.velocity = new Vector2(0, 0);

        yield return delay;

        player.transform.position = windowEnter.GetExitPointPosition();
        player.Rigidbody2D.velocity = new Vector2(0, 0);

        yield return delay;

        player.PlayerMovement.SetEnabled(false);
        windowEnter.PlayerWait(player);
    }

    public void ExitWindow(PlayerFSM player)
    {
        spriteRenderer.sprite = openedWithRopeSprite;
        currentWindowExitState = WindowExitState.OpenedWithRope;
        player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Visible);
        player.VirtualCamera.gameObject.SetActive(true);
        windowEnter.SetEnabledVirtualCamera(false);
        SetEnabledVirtualCamera(false);
    }

    public void Interact(PlayerFSM player)
    {
        switch (currentWindowExitState)
        {
            case WindowExitState.Closed:
                break;
            case WindowExitState.PlayerInside:
                break;
            case WindowExitState.OpenedWithRope:
                StartCoroutine(EnterWindowAnimation(player));
                break;
        }
    }

    public void SetEnabledVirtualCamera(bool enabled)
    {
        virtualCamera.gameObject.SetActive(enabled);
    }
    public void SetVirtualCameraPriority(int priority)
    {
        virtualCamera.Priority = priority;
    }
}
