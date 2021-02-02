using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.HidePlaces;
using System.Collections;
using UnityEngine;

namespace HeyEscape.Core.Player
{
    public class PlayerHideHandle : MonoBehaviour
    {
        [SerializeField] SpriteRenderer playerRenderer;
        [SerializeField] PlayerFSM playerFSM;
        [SerializeField] VisibilityState visibilityState;
        [SerializeField] private float SearchHidePlaceRadius = 1f;
        private bool isHidden = false;
        public bool IsHidden { get => isHidden; private set => isHidden = value; }

        private Vector3 initPosition;
        private Vector3 initScale;
        private Color initColor;

        private Vector3 currentVelocityPosition;
        private Vector3 currentVelocityScale;
        private bool isMoving = false;

        private IEnumerator SmoothMovingToHidePositionCoroutine;
        private IEnumerator hidePlayerAfterAnimation;

        public void Hide(HidePlaceInfoSO hidePlaceInfo)
        {
            SaveInitialPlayerParams();
            isMoving = false;

            playerFSM.LightVision.SetVisionState(hidePlaceInfo.lightVisionState);

            if (!playerFSM.PlayerMovement.IsLookingRight)
            {
                playerFSM.PlayerMovement.Flip();
            }
            playerFSM.Animator.SetFloat("HideType", (float)hidePlaceInfo.PlayerHidingSprite);

            if (hidePlaceInfo.PlayerHidingSprite != PlayerHidingSpriteType.WithAnimation)
            {
                playerFSM.Animator.SetBool("IsHidden", true);
            }

            IsHidden = true;

            if (SmoothMovingToHidePositionCoroutine != null)
            {
                StopCoroutine(SmoothMovingToHidePositionCoroutine);
            }

            if (hidePlaceInfo.hidePlayerSpriteAfterTime)
            {
                hidePlayerAfterAnimation = hidePlayerAfterAnimationCoroutine(hidePlaceInfo);
                StartCoroutine(hidePlayerAfterAnimation);
            }

            SmoothMovingToHidePositionCoroutine = SmoothMovingToHidePosition(hidePlaceInfo);
            StartCoroutine(SmoothMovingToHidePositionCoroutine);
            playerRenderer.color = hidePlaceInfo.color;
            visibilityState.SetVisibilityState(hidePlaceInfo.visibilityState);
        }

        IEnumerator hidePlayerAfterAnimationCoroutine(HidePlaceInfoSO hidePlaceInfo)
        {
            yield return new WaitForSeconds(hidePlaceInfo.delayBeforeHiddingPlayerSprite);
            playerRenderer.enabled = false;
        }

        public void Unhide()
        {
            if (SmoothMovingToHidePositionCoroutine != null)
            {
                StopCoroutine(SmoothMovingToHidePositionCoroutine);
            }

            if (hidePlayerAfterAnimation != null)
            {
                StopCoroutine(hidePlayerAfterAnimation);
            }

            playerRenderer.enabled = true;

            playerFSM.Animator.SetBool("IsHidden", false);

            isMoving = false;
            transform.localScale = initScale;
            playerRenderer.color = initColor;
            visibilityState.SetVisibilityState(VisibilityState.State.Visible);
            playerFSM.LightVision.SetVisionState(PlayerLightVision.VisionState.Full);
            IsHidden = false;
        }

        IEnumerator SmoothMovingToHidePosition(HidePlaceInfoSO hidePlaceInfo)
        {
            isMoving = true;
            while (transform.position != hidePlaceInfo.transform && transform.localScale != hidePlaceInfo.scale)
            {
                if (!isMoving) break;

                transform.position = Vector3.SmoothDamp(transform.position, hidePlaceInfo.transform, ref currentVelocityPosition, .15f);
                transform.localScale = Vector3.SmoothDamp(transform.localScale, hidePlaceInfo.scale, ref currentVelocityScale, .15f);
                yield return null;
            }
            isMoving = false;
        }
        IEnumerator SmoothMovingToInitPosition()
        {
            isMoving = true;
            while (transform.position != initPosition && transform.localScale != initScale)
            {
                if (!isMoving) break;

                transform.position = Vector3.SmoothDamp(transform.position, initPosition, ref currentVelocityPosition, 1f);
                transform.localScale = Vector3.SmoothDamp(transform.localScale, initScale, ref currentVelocityScale, 1f);
                yield return null;
            }
            isMoving = false;
        }

        private void SaveInitialPlayerParams()
        {
            initPosition = transform.position;
            initScale = transform.localScale;
            initColor = playerRenderer.color;
        }
    }
}