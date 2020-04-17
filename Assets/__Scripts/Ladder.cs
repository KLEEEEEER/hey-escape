using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]private float speed = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterController2D characterController2D = collision.GetComponent<CharacterController2D>();
        if (characterController2D == null) return;
        //characterController2D.isOnLadder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CharacterController2D characterController2D = collision.GetComponent<CharacterController2D>();
        if (characterController2D == null) return;
        //characterController2D.isOnLadder = false;
    }
}
