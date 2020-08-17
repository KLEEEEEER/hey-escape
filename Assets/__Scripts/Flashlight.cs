using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Transform pointARectTrigger;
    [SerializeField] Transform pointBRectTrigger;
    Collider2D[] colliders;
    PlayerMovement playerMovement;
    

    private void FixedUpdate()
    {
        colliders = Physics2D.OverlapAreaAll(pointARectTrigger.position, pointBRectTrigger.position);
        playerMovement = null;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            PlayerMovement playerMovementTemp = collider.GetComponent<PlayerMovement>();
            if (playerMovementTemp != null)
            {
                playerMovement = playerMovementTemp;
                break;
            }
        }

        if (playerMovement != null && !GameManager.instance.IsGameOver)
        {
            if (GameManager.instance.PlayerComponent.isPlayerHidden() && enemy.isFacingRight() && !playerMovement.isVisibleLeft) return;
            if (GameManager.instance.PlayerComponent.isPlayerHidden() && !enemy.isFacingRight() && !playerMovement.isVisibleRight) return;

            GameManager.instance.GameOver();
            enemy.OnGameOver();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointARectTrigger.position, pointBRectTrigger.position);
    }
}
