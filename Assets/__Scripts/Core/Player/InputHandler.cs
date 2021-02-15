using HeyEscape.Core.Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace HeyEscape.Core.Player
{
    public class InputHandler : MonoBehaviour
    {
        public UnityEvent KillButtonPressed = new UnityEvent();
        public UnityEvent UsingButtonPressed = new UnityEvent();
        public UnityEvent JumpButtonPressed = new UnityEvent();
        public UnityEvent PauseButtonPressed = new UnityEvent();

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        public UnityEvent GodModButtonPressed = new UnityEvent();
#endif

        public TouchRayEvent ScreenTouched = new TouchRayEvent();

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        [SerializeField] Joystick joystick;

        private bool isEnabled = true;
        private bool isEnabledPauseButton = true;

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
            #region Cheats
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (Input.GetKeyDown(KeyCode.P))
            {
                GodModButtonPressed?.Invoke();
            }
#endif
            #endregion



            if (!isEnabled) return;

#if UNITY_EDITOR
            float HorizontalKeyboard = Input.GetAxisRaw("Horizontal");
            float VerticalKeyboard = Input.GetAxisRaw("Vertical");
            float HorizontalJoystick = Mathf.Clamp(joystick.Horizontal * 2.5f, -1f, 1f);
            float VerticalJoystick = Mathf.Clamp(joystick.Vertical * 2.5f, -1f, 1f);

            Horizontal = (HorizontalJoystick != 0) ? HorizontalJoystick : HorizontalKeyboard;
            Vertical = (VerticalJoystick != 0) ? VerticalJoystick : VerticalKeyboard;

            if (Input.GetKeyDown(KeyCode.E))
            {
                KillButtonPressed?.Invoke();
                UsingButtonPressed?.Invoke();
            }
#else
            Horizontal = Mathf.Clamp(joystick.Horizontal * 2.5f, -1f, 1f);
            Vertical = Mathf.Clamp(joystick.Vertical * 2.5f, -1f, 1f);
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
            //Debug.Log($"Vertical = {Vertical}");
        }

        public Vector2 GetDirection()
        {
            return new Vector2(Horizontal, Vertical);
        }
        public void OnJump()
        {
            if (!isEnabled) return;

            JumpButtonPressed?.Invoke();
            Vibration.Vibrate(20);
        }
        public void OnKill()
        {
            if (!isEnabled) return;

            KillButtonPressed?.Invoke();
            Vibration.Vibrate(20);
        }
        public void OnUsing()
        {
            if (!isEnabled) return;

            UsingButtonPressed?.Invoke();
            Vibration.Vibrate(20);
        }

        public void OnPause()
        {
            if (!isEnabledPauseButton) return;

            PauseButtonPressed?.Invoke();
            Vibration.Vibrate(20);
        }

        public void SetEnabled(bool enabled)
        {
            isEnabled = enabled;
        }

        public void SetEnabledPauseButton(bool enabled)
        {
            isEnabledPauseButton = enabled;
        }
    }

    [System.Serializable] public class TouchRayEvent : UnityEvent<RaycastHit2D> { }
}