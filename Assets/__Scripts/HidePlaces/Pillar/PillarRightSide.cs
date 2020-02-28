using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarRightSide : MonoBehaviour, IHidePlace
{
    [SerializeField] private Color HideColor;

    private bool isHidden = false;
    private GameObject playerObj;
    private Vector3 currentVelocity;
    [SerializeField] private float smoothTime;

    private void Update()
    {
        if (isHidden && playerObj != null && playerObj.transform.position != transform.position)
        {
            playerObj.transform.position = Vector3.SmoothDamp(playerObj.transform.position, transform.position, ref currentVelocity, smoothTime);
        }
    }

    public void Hide(GameObject player)
    {
        playerObj = player;
        GameManager.instance.PlayerMovement.isVisibleRight = true;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRenderer.color = HideColor;
        isHidden = true;
    }
    public bool IsAccessible()
    {
        return true;
    }

    public void Unhide(GameObject player)
    {
        playerObj = null;
        GameManager.instance.PlayerMovement.isVisibleRight = false;
        player.transform.position = transform.position;
        GameManager.instance.PlayerRenderer.color = GameManager.instance.PlayerInitialColor;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        GameManager.instance.PlayerMovement.disableMovement = false;
        isHidden = false;
    }
}
