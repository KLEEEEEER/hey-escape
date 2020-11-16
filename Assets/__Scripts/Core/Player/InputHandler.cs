using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player
{
    public class InputHandler : MonoBehaviour
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        [SerializeField] Joystick joystick;

        void Start()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            joystick.gameObject.SetActive(true);
#else
        joystick.gameObject.SetActive(false);
#endif
        }

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            Horizontal = joystick.Horizontal;
            Vertical = joystick.Vertical;
#else
        //Horizontal = Input.GetAxisRaw("Horizontal") * speed;
        //Vertical = Input.GetAxisRaw("Vertical") * climbingSpeed;
#endif
        }
    }
}