using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDesk : MonoBehaviour, IHidePlace
{
    [SerializeField] private bool isHidden = false;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite withoutPlayer;
    [SerializeField] private Sprite withPlayer;

    private void Update()
    {
        if (isHidden && GameManager.instance.Player.gameObject != null && GameManager.instance.Player.gameObject.transform.position != transform.position)
        {
            GameManager.instance.Player.gameObject.transform.position = transform.position;
        }
    }
    public void Hide()
    {
        GameManager.instance.PlayerFSM.TransitionToState(GameManager.instance.PlayerFSM.DisableState);
        GameManager.instance.PlayerComponent.HidePlayer();
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.enabled = false;
        spriteRenderer.sprite = withPlayer;
        isHidden = true;
    }

    public void Unhide()
    {
        GameManager.instance.PlayerFSM.TransitionToState(GameManager.instance.PlayerFSM.IdleState);
        GameManager.instance.PlayerComponent.UnhidePlayer();
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRenderer.enabled = true;
        spriteRenderer.sprite = withoutPlayer;
        isHidden = false;
    }

    public bool IsAccessible()
    {
        return true;
    }
}
