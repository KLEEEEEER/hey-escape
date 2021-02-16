using HeyEscape.Core.Player.FSM;
using System.Collections;
using UnityEngine;

namespace HeyEscape.Interactables.HidePlaces
{
    public class ReceptionDesk : MonoBehaviour, IHidePlace
    {
        [SerializeField] private bool isHidden = false;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite withoutPlayer;
        [SerializeField] private Sprite withPlayer;

        [SerializeField] private Transform climbPoint;

        private WaitForSeconds delay = new WaitForSeconds(0.5f);
        private Vector3 currentVelocity;

        IEnumerator hideCoroutine;

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
            while ((player.transform.position - climbPoint.position).sqrMagnitude > 0.2f*0.2f)
            {
                player.transform.position = Vector3.SmoothDamp(player.transform.position, climbPoint.position, ref currentVelocity, 0.1f);
                yield return null;
            }
            //player.transform.position = climbPoint.position;
            player.Animator.ResetTrigger("FromAnyToIdle");
            player.Animator.ResetTrigger("WindowClimbing");
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