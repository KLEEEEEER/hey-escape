using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Interactables.HidePlaces
{
    public class ReceptionDesk : MonoBehaviour, IHidePlace // IInteractable,
    {
        [SerializeField] private bool isHidden = false;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite withoutPlayer;
        [SerializeField] private Sprite withPlayer;

        [SerializeField] private Transform climbPoint;


        private WaitForSeconds delay = new WaitForSeconds(0.5f);

        IEnumerator hideCoroutine;

        /*public void Interact(PlayerFSM player)
        {
            StartCoroutine(hideCoroutine(player));
        }

        IEnumerator hideCoroutine(PlayerFSM player)
        {
            player.TransitionToState(player.DisableState);
            player.Visibility.SetVisibilityState(HeyEscape.Core.Player.VisibilityState.State.Hidden);
            //player.PlayerMovement.SetEnabled(true);

            player.transform.position = climbPoint.position;
            player.Rigidbody2D.velocity = new Vector2(0, 0);
            player.Animator.SetTrigger("WindowClimbing");
            player.LightVision.SetVisionState(HeyEscape.Core.Player.PlayerLightVision.VisionState.InDoor);

            yield return delay;

            player.Renderer.enabled = false;
        }*/

        public bool IsAccessible()
        {
            return true;
        }

        [SerializeField] private HidePlaceInfoSO hidePlaceInfoSO;
        public HidePlaceInfoSO GetHidePlaceInfo()
        {
            hidePlaceInfoSO.transform = transform.position;
            return hidePlaceInfoSO;
        }

        public void OnHide(PlayerFSM player)
        {
            hideCoroutine = onHideCoroutine(player);
            StartCoroutine(hideCoroutine);
        }

        IEnumerator onHideCoroutine(PlayerFSM player)
        {
            player.transform.position = climbPoint.position;
            player.Animator.SetTrigger("WindowClimbing");
            yield return new WaitForSeconds(hidePlaceInfoSO.delayBeforeHiddingPlayerSprite);
            spriteRenderer.sprite = withPlayer;
        }

        public void OnUnhide(PlayerFSM player)
        {
            StopCoroutine(hideCoroutine);
            player.Animator.SetTrigger("FromAnyToIdle");
            spriteRenderer.sprite = withoutPlayer;
        }
    }
}