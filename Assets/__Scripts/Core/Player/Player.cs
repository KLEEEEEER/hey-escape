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
        /*public VisibilityState visibility;
        [SerializeField] private DetectorHandler detectorHandler;
        [SerializeField] private Animator animator;

        public event Action<string> OnPlayerInteractEvent;

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

        if (Input.GetKeyDown(KeyCode.W) && hideplaces.Count > 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsGrounded", true);
            if (hideplaces[0].IsAccessible())
            {
                hideplaces[0].Hide();
                currentHidePlace = hideplaces[0];
            }
        }
        if (Input.GetKeyDown(KeyCode.S) && currentHidePlace != null)
        {
            currentHidePlace.Unhide();
            currentHidePlace = null;
        }
#endif


#if UNITY_ANDROID || UNITY_IPHONE
            *//*if (GameManager.instance.PlayerFSM.InputHandler.Vertical > 0.8f && !detectorHandler.IsHidden())
            {
                detectorHandler.TryHideInHidePlace(() => 
                {
                    animator.SetBool("IsJumping", false);
                    animator.SetBool("IsRunning", false);
                    animator.SetBool("IsGrounded", true);
                });
            } 
            else if (GameManager.instance.PlayerFSM.InputHandler.Vertical < 0.8f && detectorHandler.IsHidden())
            {
                detectorHandler.UnhideFromHidePlace();
            }*//*
#endif
        }*/
    }
}