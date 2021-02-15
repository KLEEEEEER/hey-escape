using HeyEscape.Core.Player.FSM;
using UnityEditor;
using UnityEngine;

namespace HeyEscape.EditorBehaviour
{
    [CustomEditor(typeof(PlayerFSM))]
    public class PlayerFSMLabel : Editor
    {
        private void OnSceneGUI()
        {
            if (!Application.isPlaying) return;

            PlayerFSM player = (PlayerFSM)target;

            if (player == null)
            {
                return;
            }

            GUIStyle colorStyle = new GUIStyle();
            colorStyle.normal.textColor = Color.white;
            colorStyle.alignment = TextAnchor.MiddleLeft;

            Handles.Label(player.transform.position + Vector3.up, player.Visibility.currentState.ToString() + "\n" + player.CurrentState.ToString(), colorStyle);
        }
    }
}