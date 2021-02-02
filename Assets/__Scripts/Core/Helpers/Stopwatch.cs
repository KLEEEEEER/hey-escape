using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Helpers
{
    public class Stopwatch : MonoBehaviour
    {
        private float value = 0f;
        private bool isCounting = false;

        private void Update()
        {
            if (isCounting)
                value += Time.deltaTime;
        }

        public void StartCounting()
        {
            value = 0f;
            isCounting = true;
        }

        public void Pause()
        {
            isCounting = false;
        }

        public void Continue()
        {
            isCounting = true;
        }

        public float GetCurrentValue()
        {
            return value;
        }
    }
}