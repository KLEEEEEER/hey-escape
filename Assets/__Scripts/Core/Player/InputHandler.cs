using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HeyEscape.Core.Player
{
    public class InputHandler : MonoBehaviour
    {
        public UnityEvent KillButtonPressed = new UnityEvent();
        public UnityEvent UsingButtonPressed = new UnityEvent();
        public UnityEvent JumpButtonPressed = new UnityEvent();
        public TouchRayEvent ScreenTouched = new TouchRayEvent();

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
#if UNITY_EDITOR
            //Horizontal = Input.GetAxisRaw("Horizontal");
            //Vertical = Input.GetAxisRaw("Vertical");

            Horizontal = Mathf.Clamp(joystick.Horizontal * 2.5f, -1f, 1f);
            Vertical = Mathf.Clamp(joystick.Vertical * 2.5f, -1f, 1f);

            if (Input.GetKeyDown(KeyCode.E))
            {
                KillButtonPressed?.Invoke();
                UsingButtonPressed?.Invoke();
            }
#elif UNITY_ANDROID || UNITY_IPHONE
            Horizontal = Mathf.Clamp(joystick.Horizontal * 2.5f, -1f, 1f);
            Vertical = Mathf.Clamp(joystick.Vertical * 2.5f, -1f, 1f);
#else
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
#endif

            /*if (Input.touchCount > 0)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    ScreenTouched?.Invoke(hit);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    ScreenTouched?.Invoke(hit);
                }
            }*/

        }

        public Vector2 GetDirection()
        {
            return new Vector2(Horizontal, Vertical);
        }
        public void OnJump()
        {
            JumpButtonPressed?.Invoke();
        }
        public void OnKill()
        {
            KillButtonPressed?.Invoke();
        }
        public void OnUsing()
        {
            UsingButtonPressed?.Invoke();
        }
    }

    [System.Serializable] public class TouchRayEvent : UnityEvent<RaycastHit2D> { }
}