using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] Transform startPosition;

    void Start()
    {
        //GameManager.instance.PlayerComponent.HidePlayer();
        //GameManager.instance.Player.position = startPosition.position;
        //GameManager.instance.PlayerComponent.UnhidePlayer();
    }
}
