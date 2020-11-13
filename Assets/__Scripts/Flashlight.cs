using Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] Transform pointARectTrigger;
    [SerializeField] Transform pointBRectTrigger;
    Collider2D[] colliders;
    InputHandler playerMovement;



    private void FixedUpdate()
    {
        colliders = Physics2D.OverlapAreaAll(pointARectTrigger.position, pointBRectTrigger.position);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            if (collider.CompareTag("Player") && !GameManager.instance.IsGameOver)
            {
                if (GameManager.instance.PlayerComponent.isPlayerHidden() && enemy.isFacingRight() && !playerMovement.isVisibleLeft) return;
                if (GameManager.instance.PlayerComponent.isPlayerHidden() && !enemy.isFacingRight() && !playerMovement.isVisibleRight) return;

                GameManager.instance.GameOver();
                enemy.OnGameOver();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointARectTrigger.position, pointBRectTrigger.position);
    }
}
