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

        GameManager.instance.PlayerMovement.disableMovement = false;
        windowExit.PlayerWait();
    }

    void OpenWindow()
    {
        spriteRenderer.sprite = openedSprite;
        openingSound.Play();
    }

    void SetRope()
    {
        Inventory.instance.UseItem(typeof(Rope));
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

    public void Interact()
    {
        switch (windowEnterState)
        {
            case WindowEnterState.Closed:
                OpenWindow();
                windowEnterState = WindowEnterState.Opened;
                break;
            case WindowEnterState.Opened:
                if (Inventory.instance.HasItem(typeof(Rope)) && windowExit != null)
                {
                    SetRope();
                    windowEnterState = WindowEnterState.OpenedWithRope;
                }
                break;
            case WindowEnterState.PlayerInside:
                break;
            case WindowEnterState.OpenedWithRope:
                if (windowExit != null)
                    StartCoroutine(EnterWindowAnimation());
                break;
        }
    }

    public void PlayerWait()
    {
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.InWindowState);
        spriteRenderer.sprite = openedWithRopeAndPlayerSprite;
        windowEnterState = WindowEnterState.PlayerInside;
        GameManager.instance.CharacterController2D.InWindowState.OnWindowExit.AddListener(ExitWindow);
    }

    public void ExitWindow()
    {
        windowEnterState = WindowEnterState.OpenedWithRope;
        spriteRenderer.sprite = openedWithRopeSprite;
    }

    public Vector3 GetExitPointPosition() => ExitPoint.position;
}
