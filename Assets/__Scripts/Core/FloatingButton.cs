using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingButton : MonoBehaviour
{
    Vector3 posOffset;
    Vector3 tempPos;
    [SerializeField] float amplitude = 0.5f;
    [SerializeField] float frequency = 1f;
    [SerializeField] Vector3 offsetPosition = new Vector3(0, 1.2f, 0);
    [SerializeField] Transform player;

    private void Start()
    {
        //posOffset = player.position;
    }

    private void Update()
    {
        tempPos = player.position + offsetPosition;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
