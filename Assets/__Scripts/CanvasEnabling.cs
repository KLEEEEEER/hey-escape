using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnabling : MonoBehaviour
{
    [SerializeField] private Transform CanvasPosition;
    private void OnEnable()
    {
        transform.position = CanvasPosition.position;
    }
}
