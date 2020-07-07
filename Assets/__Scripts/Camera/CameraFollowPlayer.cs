using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void Update()
    {
        if (player != null)
            transform.position = player.transform.position;
    }
}
