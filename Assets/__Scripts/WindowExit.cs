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

    public void PlayerWait()
    {
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.InWindowState);
        GameManager.instance.CharacterController2D.InWindowState.OnWindowExit.AddListener(ExitWindow);
        spriteRenderer.sprite = openedWithRopeWithPlayerSprite;
        currentWindowExitState = WindowExitState.PlayerInside;
    }

    public Vector3 GetExitPointPosition()
    {
        return ExitPoint.position;
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

        yield return delay;

        GameManager.instance.Player.position = windowEnter.GetExitPointPosition();
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);

        yield return delay;

        GameManager.instance.PlayerMovement.disableMovement = false;
        windowEnter.PlayerWait();
    }

    public void ExitWindow()
    {
        spriteRenderer.sprite = openedWithRopeSprite;
        currentWindowExitState = WindowExitState.OpenedWithRope;
    }

    public void Interact()
    {
        switch (currentWindowExitState)
        {
            case WindowExitState.Closed:
                break;
            case WindowExitState.PlayerInside:
                break;
            case WindowExitState.OpenedWithRope:
                StartCoroutine(EnterWindowAnimation());
                break;
        }
    }
}
