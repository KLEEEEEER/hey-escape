using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IHidePlace
{
    [SerializeField] private Vector3 PlayerHideScale = new Vector3(0.9f, 0.9f, 0.9f);
    [SerializeField] private Color HideColor;
    [SerializeField] private float smoothTime;

    private bool isHidden = false;
    private GameObject playerObj;
    private Vector3 currentVelocity;

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
        //player.transform.position = transform.position;
        player.transform.localScale = PlayerHideScale;
        GameManager.instance.PlayerRenderer.color = HideColor;
        GameManager.instance.PlayerMovement.disableMovement = true;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        isHidden = true;
    }
    public bool IsAccessible()
    {
        return true;
    }

    public void Unhide(GameObject player)
    {
        playerObj = null;
        player.transform.position = transform.position;
        player.transform.localScale = GameManager.instance.PlayerInitialScale;
        GameManager.instance.PlayerRenderer.color = GameManager.instance.PlayerInitialColor;
        GameManager.instance.PlayerMovement.disableMovement = false;
        GameManager.instance.PlayerRigidbody.velocity = new Vector2(0, 0);
        isHidden = false;
    }
}
