using Cinemachine;
using HeyEscape.Core.Interfaces;
using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.GameItems;
using System.Collections;
using UnityEngine;

namespace HeyEscape.Interactables.Environment
{
    public class WindowEnter : MonoBehaviour, IInteractable
    {
        [SerializeField] bool canBeOpened = true;
        [SerializeField] WindowExit windowExit;
        [SerializeField] WindowExit[] windowsExitToDropRope;
        [SerializeField] Sprite openedSprite;
        [SerializeField] Sprite openedWithRopeSprite;
        [SerializeField] Sprite openedWithRopeAndPlayerSprite;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Transform ExitPoint;
        [SerializeField] AudioSource openingSound;
        [SerializeField] CinemachineVirtualCamera virtualCamera;

        [SerializeField] Transform climbPoint;

        private WaitForSeconds delay = new WaitForSeconds(0.5f);
        private Vector3 currentVelocity;

        //[SerializeField] Player playerNear;
        enum WindowEnterState { Closed, Opened, OpenedWithRope, PlayerInside }

        [SerializeField] WindowEnterState windowEnterState = WindowEnterState.Closed;

        private void Start()
        {
            switch (windowEnterState)
            {
                case WindowEnterState.Closed:
                    break;
                case WindowEnterState.Opened:
                    OpenWindow();
                    break;
                case WindowEnterState.OpenedWithRope:
                    SetRope();
                    break;
            }
        }

        IEnumerator EnterWindowAnimation(PlayerFSM player)
        {
            SetVirtualCameraPriority(11);
            windowExit.SetVirtualCameraPriority(12);

            player.TransitionToState(player.DisableState);
            player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Hidden);
            player.PlayerMovement.SetEnabled(true);

            while ((player.transform.position - climbPoint.position).sqrMagnitude > 0.2f * 0.2f)
            {
                player.transform.position = Vector3.SmoothDamp(player.transform.position, climbPoint.position, ref currentVelocity, 0.1f);
                yield return null;
            }

            windowExit.SetEnabledVirtualCamera(true);
            player.VirtualCamera.gameObject.SetActive(false);

            player.Rigidbody2D.velocity = new Vector2(0, 0);

            player.Animator.SetTrigger("WindowClimbing");
            player.LightVision.SetVisionState(HeyEscape.Core.Player.PlayerLightVision.VisionState.InDoor);

            yield return delay;


            if (player.Renderer != null) player.Renderer.enabled = false;
            player.transform.position = windowExit.GetExitPointPosition();
            player.Rigidbody2D.velocity = new Vector2(0, 0);

            yield return delay;

            windowExit.PlayerFallAnimation(0.5f);

            yield return delay;

            windowExit.OpenWindow();

            yield return delay;

            player.LightVision.SetVisionState(HeyEscape.Core.Player.PlayerLightVision.VisionState.Full);
            player.PlayerMovement.SetEnabled(false);
            windowExit.PlayerWait(player);
        }

        void OpenWindow()
        {
            spriteRenderer.sprite = openedSprite;
            openingSound.Play();
        }

        void SetRope(PlayerFSM player = null)
        {
            if (player != null)
            {
                player.Inventory.UseItem(typeof(Rope));
            }

            spriteRenderer.sprite = openedWithRopeSprite;
            windowExit.DropRope();
            if (windowsExitToDropRope.Length > 0)
            {
                foreach (WindowExit window in windowsExitToDropRope)
                {
                    window.DropRope();
                }
            }
        }

        public void Interact(PlayerFSM player)
        {
            if (!canBeOpened) return;

            switch (windowEnterState)
            {
                case WindowEnterState.Closed:
                    OpenWindow();
                    windowEnterState = WindowEnterState.Opened;
                    break;
                case WindowEnterState.Opened:
                    if (player.Inventory.HasItem(typeof(Rope)) && windowExit != null)
                    {
                        SetRope(player);
                        windowEnterState = WindowEnterState.OpenedWithRope;
                    }
                    break;
                case WindowEnterState.PlayerInside:
                    break;
                case WindowEnterState.OpenedWithRope:
                    if (windowExit != null)
                        StartCoroutine(EnterWindowAnimation(player));
                    break;
            }
        }

        public void PlayerWait(PlayerFSM player)
        {
            player.TransitionToState(player.InWindowState);
            spriteRenderer.sprite = openedWithRopeAndPlayerSprite;
            windowEnterState = WindowEnterState.PlayerInside;
            player.InWindowState.OnWindowExit.AddListener(ExitWindow);
        }

        public void ExitWindow(PlayerFSM player)
        {
            windowEnterState = WindowEnterState.OpenedWithRope;
            spriteRenderer.sprite = openedWithRopeSprite;
            player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Visible);
            player.VirtualCamera.gameObject.SetActive(true);
            windowExit.SetEnabledVirtualCamera(false);
            SetEnabledVirtualCamera(false);
        }

        public Vector3 GetExitPointPosition() => ExitPoint.position;

        public void SetEnabledVirtualCamera(bool enabled)
        {
            virtualCamera.gameObject.SetActive(enabled);
        }
        public void SetVirtualCameraPriority(int priority)
        {
            virtualCamera.Priority = priority;
        }
    }
}