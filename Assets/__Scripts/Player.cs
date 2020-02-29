using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    [SerializeField] float interactibleDetectionRadius = 0.4f;
    [SerializeField] private float enemyDetectionRadius = 1;
    [SerializeField] private Transform enemyDetectionPosition;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;

    [SerializeField] bool isHidden = false;

    Collider2D[] colliders;

    //public bool nearHidePlace = false;
    //private IHidePlace hidePlace;
    List<IInteractable> interactables;
    List<ISearchable> searchables;
    List<IKillable> killables;

    public event Action<string> OnPlayerInteractEvent;

    private void Start()
    {
        interactables = new List<IInteractable>();
        searchables = new List<ISearchable>();
        killables = new List<IKillable>();
    }

    void Update()
    {
        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Kill");
            foreach (IKillable killable in killables)
            {
                killable.Kill();
            }
        }

        if (!playerMovement.isPlayerMovingDisabled() && Input.GetKeyDown(KeyCode.E))
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (IInteractable interactable in interactables)
            {
                interactable.Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        interactables.Clear();
        killables.Clear();
        searchables.Clear();

        colliders = Physics2D.OverlapCircleAll(enemyDetectionPosition.position, enemyDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            IKillable killable = collider.GetComponent<IKillable>();
            if (killable != null)
            {
                killables.Add(killable);
            }

            ISearchable searchable = collider.GetComponent<ISearchable>();
            if (searchable != null)
            {
                Debug.Log("Found searchable");
                searchables.Add(searchable);
            }
        }

        colliders = Physics2D.OverlapCircleAll(transform.position, interactibleDetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;

            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactables.Add(interactable);
            }
        }

        Debug.Log("Found " + searchables.Count + " ISearchable");
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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyNear = isEnemyNearCheck(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyNear = isEnemyNearCheck(collision);
    }

    private Enemy isEnemyNearCheck(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) Debug.Log("Enemy near");
        else Debug.Log("Enemy left");
        return (enemy != null) ? enemy : null;
    }*/
}
