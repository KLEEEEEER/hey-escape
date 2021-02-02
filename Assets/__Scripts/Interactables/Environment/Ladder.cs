using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Interactables.Environment
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private float speed = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerFSM characterController2D = collision.GetComponent<PlayerFSM>();
            if (characterController2D == null) return;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            PlayerFSM characterController2D = collision.GetComponent<PlayerFSM>();
            if (characterController2D == null) return;
        }
    }
}