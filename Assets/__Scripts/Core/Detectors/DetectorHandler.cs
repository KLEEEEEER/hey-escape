using HeyEscape.Core.Player;
using HeyEscape.Core.Player.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class DetectorHandler : MonoBehaviour
    {
        [SerializeField] float interactibleDetectionRadius = 0.4f;
        [SerializeField] private float enemyDetectionRadius = 1;
        [SerializeField] private Transform enemyDetectionPosition;
        [SerializeField] PlayerHideHandle playerHideHandle;

        PlayerFSM player;

        Collider2D[] colliders = new Collider2D[10];
        int amount = 0;

        Detector<IInteractable> interactableDetector;
        Detector<IHidePlace> hideplaceDetector;
        Detector<ISearchable> searchableDetector;
        Detector<IKillable> killableDetector;

        private bool initialized = false;
        private IHidePlace foundHidePlace;

        public void Initialize(PlayerFSM player)
        {
            if (initialized) return;

            this.player = player;

            interactableDetector = new InteractableDetector(this.player);
            searchableDetector = new SearchableDetector(this.player.Inventory);
            hideplaceDetector = new HidePlaceDetector();
            killableDetector = new KillableDetector();

            initialized = true;
        }
        private void FixedUpdate()
        {
            if (!initialized) return;

            amount = Physics2D.OverlapCircleNonAlloc(enemyDetectionPosition.position, enemyDetectionRadius, colliders);
            killableDetector.CheckCollidersInArray(colliders, amount);
            searchableDetector.CheckCollidersInArray(colliders, amount);

            amount = Physics2D.OverlapCircleNonAlloc(transform.position, interactibleDetectionRadius, colliders);
            interactableDetector.CheckCollidersInArray(colliders, amount);
            hideplaceDetector.CheckCollidersInArray(colliders, amount);
        }

        public void InteractKillable(Action onInteract = null)
        {
            killableDetector.InteractWithFoundColliders(onInteract);
        }

        public void InteractSearchable(Action onInteract = null)
        {
            searchableDetector.InteractWithFoundColliders(onInteract);
        }

        public void InteractInteractable(Action onInteract = null)
        {
            interactableDetector.InteractWithFoundColliders(onInteract);
        }

        public bool TryHideInHidePlace(Action onInteract = null)
        {
            if (playerHideHandle.IsHidden) return false;
            if (hideplaceDetector.GetDetectedCollidersCount() == 0) return false;

            foundHidePlace = hideplaceDetector.GetFirstFoundObject();
            if (foundHidePlace.IsAccessible())
            {
                onInteract?.Invoke();
                foundHidePlace.OnHide(player);
                playerHideHandle.Hide(foundHidePlace.GetHidePlaceInfo());
                return true;
            }

            return false;
        }

        public void UnhideFromHidePlace()
        {
            if (foundHidePlace != null)
            {
                foundHidePlace.OnUnhide(player);
                foundHidePlace = null;
            }
            playerHideHandle.Unhide();
        }

        public bool IsHidden()
        {
            return playerHideHandle.IsHidden;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyDetectionPosition.position, enemyDetectionRadius);

            Gizmos.DrawWireSphere(transform.position, interactibleDetectionRadius);
        }
    }
}