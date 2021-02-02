using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.Core.Game
{
    public class BeforePlayTimer : MonoBehaviour
    {
        [Header("Attributes")]
        private WaitForSeconds secondDelay = new WaitForSeconds(1f);

        [Header("Components")]
        [SerializeField] private Text countDownTimer;

        public void StartCountdown(int seconds, Action onEnd = null)
        {
            StartCoroutine(StartingCountdown(seconds, onEnd));
        }

        IEnumerator StartingCountdown(int seconds, Action onEnd = null)
        {
            countDownTimer.gameObject.SetActive(true);
            for (int i = seconds; i > 0; i--)
            {
                countDownTimer.text = i.ToString();
                yield return secondDelay;
            }
            countDownTimer.gameObject.SetActive(false);
            onEnd.Invoke();
        }
    }
}