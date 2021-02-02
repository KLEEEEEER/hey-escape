using HeyEscape.Core.Game;
using HeyEscape.Core.Player;
using HeyEscape.Core.Player.FSM;
using UnityEngine;

namespace HeyEscape.Core.Enemy
{
    public class EnemyFlashlight : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        [SerializeField] Transform pointARectTrigger;
        [SerializeField] Transform pointBRectTrigger;
        Collider2D[] colliders;
        InputHandler playerMovement;

        private bool isCaughtPlayer = false;

        private void FixedUpdate()
        {
            if (isCaughtPlayer) return;

            colliders = Physics2D.OverlapAreaAll(pointARectTrigger.position, pointBRectTrigger.position);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;
                if (collider.CompareTag("Player") && !GameStateChanger.Instance.CompareState(GameStateChanger.GameState.GameOver))
                {
                    PlayerFSM playerFSM = collider.GetComponent<PlayerFSM>();

                    if (playerFSM != null)
                    {
                        if (enemy.isFacingRight() && !playerFSM.Visibility.CheckVisibility(VisibilityState.State.VisibleLeft)) return;
                        if (!enemy.isFacingRight() && !playerFSM.Visibility.CheckVisibility(VisibilityState.State.VisibleRight)) return;
                    }

                    GameStateChanger.Instance.SetState(GameStateChanger.GameState.GameOver);
                    enemy.OnGameOver();

                    isCaughtPlayer = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointARectTrigger.position, pointBRectTrigger.position);
        }
    }
}