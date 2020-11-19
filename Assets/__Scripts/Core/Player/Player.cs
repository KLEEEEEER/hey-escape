using Core.Detectors;
using HeyEscape.Core.Player;
using HeyEscape.Core.Player.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HeyEscape.Core.Player
{
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        public VisibilityState visibility;
        [SerializeField] private PlayerFSM playerFSM;


        [SerializeField] float interactibleDetectionRadius = 0.4f;
        [SerializeField] private float enemyDetectionRadius = 1;
        [SerializeField] private Transform enemyDetectionPosition;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Animator animator;

        [SerializeField] private GameObject eButton;
        [SerializeField] private GameObject qButton;

        [SerializeField] bool isHidden = false;

        Collider2D[] colliders = new Collider2D[10];
        int amount = 0;

        Detector<IInteractable> interactableDetector = new InteractableDetector();
        Detector<IHidePlace> hideplaceDetector = new HidePlaceDetector();
        [SerializeField] PlayerHideHandle playerHideHandle;
        Detector<ISearchable> searchableDetector;
        Detector<IKillable> killableDetector = new KillableDetector();

        public event Action<string> OnPlayerInteractEvent;

        private void Start()
        {
            searchableDetector = new SearchableDetector(Inventory.instance);
        }

        void Update()
        {
            if (GameManager.instance.IsGameOver) return;

#if UNITY_STANDALONE

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.Q) && killables.Count > 0)
        {
            foreach (IKillable killable in killables)
            {
                animator.SetTrigger("Kill");
                killable.Kill();
            }
        }

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.E) && searchables.Count > 0)
        {
            animator.SetTrigger("Search");

            //List<InventoryItem> enemyItems = new List<InventoryItem>();
            foreach (ISearchable searchable in searchables)
            {
                List<InventoryItem> enemyItems = searchable.Search();
                if (enemyItems.Count > 0)
                {
                    foreach (InventoryItem item in enemyItems)
                    {
                        Inventory.instance.AddItem(item);
                    }
                }
            }
        }

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.E))
        {
            foreach (IInteractable interactable in interactables)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsGrounded", true);
                interactable.Interact();
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && !isHidden && hideplaces.Count > 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsGrounded", true);
            if (hideplaces[0].IsAccessible())
            {
                hideplaces[0].Hide();
                currentHidePlace = hideplaces[0];
                isHidden = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && isHidden && currentHidePlace != null)
        {
            currentHidePlace.Unhide();
            currentHidePlace = null;
            isHidden = false;
        }
#endif


#if UNITY_ANDROID || UNITY_IPHONE
            if (GameManager.instance.PlayerFSM.InputHandler.Vertical > 0.8f && !playerHideHandle.IsHidden && hideplaceDetector.GetDetectedCollidersCount() > 0)
            {
                IHidePlace foundHidePlace = hideplaceDetector.GetFirstFoundObject();
                if (foundHidePlace.IsAccessible())
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsGrounded", true);
                    playerHideHandle.Hide(foundHidePlace.GetHidePlaceInfo());
                }
            }
            if (GameManager.instance.PlayerFSM.InputHandler.Vertical < 0.8f && playerHideHandle.IsHidden)
            {
                playerHideHandle.Unhide();
            }
#endif
        }

        private void FixedUpdate()
        {
            amount = Physics2D.OverlapCircleNonAlloc(enemyDetectionPosition.position, enemyDetectionRadius, colliders);
            killableDetector.CheckCollidersInArray(colliders, amount);
            searchableDetector.CheckCollidersInArray(colliders, amount);

            amount = Physics2D.OverlapCircleNonAlloc(transform.position, interactibleDetectionRadius, colliders);
            interactableDetector.CheckCollidersInArray(colliders, amount);
            hideplaceDetector.CheckCollidersInArray(colliders, amount);
        }

        private void DebugColliders(Collider2D[] colliders)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Collider2D collider in colliders)
            {
                if (collider == null) continue;
                stringBuilder.Append(collider.name);
                stringBuilder.Append(" ");
            }
            Debug.Log(stringBuilder.ToString());
        }

        public void KillButtonPressed()
        {
            if (!playerMovement.IsEnabled || GameManager.instance.IsGameOver) return;

            killableDetector.InteractWithFoundColliders(() => { animator.SetTrigger("Kill"); });
        }
        public void UsingButtonPressed()
        {
            if (!playerMovement.IsEnabled || GameManager.instance.IsGameOver) 
            {
                Debug.Log("!playerMovement.IsEnabled || GameManager.instance.IsGameOver");
                return;
            }
            searchableDetector.InteractWithFoundColliders(() => { animator.SetTrigger("Search"); });
            interactableDetector.InteractWithFoundColliders();
        }

        public void HidePlayer()
        {
            isHidden = true;
        }

        public void UnhidePlayer()
        {
            isHidden = false;
        }

        public bool isPlayerHidden()
        {
            return isHidden;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(enemyDetectionPosition.position, enemyDetectionRadius);

            Gizmos.DrawWireSphere(transform.position, interactibleDetectionRadius);
        }
    }
}