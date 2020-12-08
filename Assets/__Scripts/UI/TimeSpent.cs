using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSpent : MonoBehaviour
{
    private void OnEnable()
    {
        Text text = GetComponent<Text>();
        if (text != null)
            text.text = GameManager.instance.GetCurrentTimeString();
    }
}
