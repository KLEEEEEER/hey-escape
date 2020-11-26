using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player
{
    public class InputHandler : MonoBehaviour
    {
        public UnityEvent KillButtonPressed = new UnityEvent();
        public UnityEvent UsingButtonPressed = new UnityEvent();

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

        public Vector2 GetDirection()
        {
            return new Vector2(Horizontal, Vertical);
        }
        public void OnKill()
        {
            KillButtonPressed?.Invoke();

            /*if (!playerMovement.IsEnabled || GameManager.instance.IsGameOver) return;

            killableDetector.InteractWithFoundColliders(() => { animator.SetTrigger("Kill"); });*/
        }
        public void OnUsing()
        {
            UsingButtonPressed?.Invoke();

            /*if (!playerMovement.IsEnabled || GameManager.instance.IsGameOver)
            {
                Debug.Log("!playerMovement.IsEnabled || GameManager.instance.IsGameOver");
                return;
            }
            searchableDetector.InteractWithFoundColliders(() => { animator.SetTrigger("Search"); });
            interactableDetector.InteractWithFoundColliders();*/
        }
    }
}