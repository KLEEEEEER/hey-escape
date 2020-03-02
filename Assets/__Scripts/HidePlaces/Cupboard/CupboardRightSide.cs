using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardRightSide : MonoBehaviour, IHidePlace, IInteractable
{
    [SerializeField] private Color HideColor;

    [SerializeField] private bool isHidden = false;
    [SerializeField] private Vector3 currentVelocity;
    [SerializeField]private float smoothTime;
    private void Update()
    {
        if (isHidden && GameManager.instance.Player.gameObject != null && GameManager.instance.Player.gameObject.transform.position != transform.position)
        {
            GameManager.instance.Player.gameObject.transform.position = Vector3.SmoothDamp(GameManager.instance.Player.gameObject.transform.position, transform.position, ref currentVelocity, smoothTime);
        }
    }
    public void Hide()
    {
        GameManager.instance.PlayerComponent.HidePlayer();
        GameManager.instance.PlayerMovement.isVisibleRight = true;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.color = HideColor;
        isHidden = true;
    }

    public void Unhide()
    {
        GameManager.instance.PlayerComponent.UnhidePlayer();
        GameManager.instance.PlayerMovement.isVisibleRight = false;
        GameManager.instance.Player.gameObject.transform.position = transform.position;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRenderer.color = GameManager.instance.PlayerInitialColor;
        isHidden = false;
    }

    public void Interact()
    {
        if (!isHidden)
        {
            Hide();
        }
        else
        {
            Unhide();
        }
    }
}
