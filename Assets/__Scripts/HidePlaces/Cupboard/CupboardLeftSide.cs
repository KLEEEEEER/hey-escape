using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardLeftSide : MonoBehaviour, IHidePlace
{
    [SerializeField] private Color HideColor;

    private bool isHidden = false;
    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime;

    private void Update()
    {
        if (isHidden && GameManager.instance.Player.gameObject != null && GameManager.instance.Player.gameObject.transform.position != transform.position)
        {
            GameManager.instance.Player.gameObject.transform.position = Vector3.SmoothDamp(GameManager.instance.Player.gameObject.transform.position, transform.position, ref currentVelocity, smoothTime);
        }
    }
    public void Hide()
    {
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.DisableState);
        GameManager.instance.PlayerComponent.HidePlayer();
        GameManager.instance.PlayerMovement.isVisibleLeft = true;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.color = HideColor;
        isHidden = true;
    }

    public void Unhide()
    {
        GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.IdleState);
        GameManager.instance.PlayerComponent.UnhidePlayer();
        GameManager.instance.PlayerMovement.isVisibleLeft = false;
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRenderer.color = GameManager.instance.PlayerInitialColor;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        isHidden = false;
    }

    public bool IsAccessible()
    {
        return true;
    }
}
