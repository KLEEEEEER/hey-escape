using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IHidePlace
{
    [SerializeField] private Vector3 PlayerHideScale = new Vector3(0.9f, 0.9f, 0.9f);
    [SerializeField] private Color HideColor;
    [SerializeField] private float smoothTime;

    private bool isHidden = false;
    private Vector3 currentVelocity;

    private void Update()
    {
        if (isHidden && GameManager.instance.Player.gameObject != null && GameManager.instance.Player.gameObject.transform.position != transform.position)
        {
            GameManager.instance.Player.gameObject.transform.position = Vector3.SmoothDamp(GameManager.instance.Player.gameObject.transform.position, transform.position, ref currentVelocity, smoothTime);
        }
    }

    public void Hide()
    {
        GameManager.instance.PlayerFSM.TransitionToState(GameManager.instance.PlayerFSM.DisableState);
        GameManager.instance.PlayerComponent.HidePlayer();
        //player.transform.position = transform.position;
        GameManager.instance.Player.gameObject.transform.localScale = PlayerHideScale;
        GameManager.instance.PlayerRenderer.color = HideColor;
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        isHidden = true;
    }

    public void Unhide()
    {
        GameManager.instance.PlayerFSM.TransitionToState(GameManager.instance.PlayerFSM.IdleState);
        GameManager.instance.PlayerComponent.UnhidePlayer();
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.Player.gameObject.transform.localScale = GameManager.instance.PlayerInitialScale;
        GameManager.instance.PlayerRenderer.color = GameManager.instance.PlayerInitialColor;
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        isHidden = false;
    }
    public bool IsAccessible()
    {
        return true;
    }
}
