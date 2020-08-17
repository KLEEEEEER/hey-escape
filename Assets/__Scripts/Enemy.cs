﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Enemy : MonoBehaviour, IKillable, ISearchable
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float timeBeforeStartWalking = 2f;
    [SerializeField] private float timeBetweenWaypoints = 2f;
    [SerializeField] Animator animator;
    [SerializeField] bool facingRight = true;
    [SerializeField] GameObject heyTextCanvas;

    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] Collider2D[] collidersToDisable;
    [SerializeField] GameObject flashlight;
    [SerializeField] InventoryItem[] items;
    [SerializeField] SpriteRenderer itemSpriteRenderer;


    [SerializeField] BoxCollider2D aliveCollider;
    [SerializeField] BoxCollider2D deadCollider;

    private int waypointIndex;
    [SerializeField] bool isDead = false;
    bool caughtPlayer = false;

    [SerializeField] EnemyHeySound heySound;

    public LocalizedString hasNoItemsStringLocalized;
    private string hasNoItemsString;

    void Start()
    {
        if (items.Length > 0 && items[0].Icon != null)
        {
            itemSpriteRenderer.sprite = items[0].Icon;
        }

        StartCoroutine(Move());
        if (!facingRight) transform.Rotate(0f, 180f, 0f);

        hasNoItemsStringLocalized.RegisterChangeHandler(UpdateHasNoItemsString);
    }

    private void UpdateHasNoItemsString(string s) { hasNoItemsString = s; }

    IEnumerator Move()
    {
        if (GameManager.instance != null && GameManager.instance.IsGameOver)
        {
            yield return new WaitForSeconds(GameManager.instance.GetStartCountdownTime());
        }

        yield return new WaitForSeconds(timeBeforeStartWalking);
        while (!isDead && !caughtPlayer)
        {
            if (waypoints.Length > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

                if (transform.position.x < waypoints[waypointIndex].transform.position.x && !facingRight) Flip();
                if (transform.position.x > waypoints[waypointIndex].transform.position.x && facingRight) Flip();

                animator.SetBool("IsWalking", transform.position != waypoints[waypointIndex].transform.position);

                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    waypointIndex++;

                    if (waypointIndex > waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                    yield return new WaitForSeconds(timeBetweenWaypoints);
                }
            }

            yield return null;
        }
    }

    public void OnGameOver()
    {
        heySound.PlayHeySound();
        caughtPlayer = true;
        animator.SetBool("IsWalking", false);
        heyTextCanvas.SetActive(true);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool isFacingRight()
    {
        return facingRight;
    }

    public void Kill()
    {
        isDead = true;
        //Destroy(parentObject);
        rigidbody2D.velocity = new Vector2(0, 0);
        rigidbody2D.isKinematic = true;
        /*foreach (Collider2D collider in collidersToDisable)
        {
            collider.isTrigger = true;
        }*/
        aliveCollider.enabled = false;
        deadCollider.enabled = true;

        flashlight.SetActive(false);
        animator.SetBool("IsDead", true);
        StartCoroutine(disableAnimator(4));
    }

    IEnumerator disableAnimator(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.enabled = false;
    }

    public List<InventoryItem> Search()
    {
        if (!isDead) return new List<InventoryItem>();

        List<InventoryItem> tempItems = new List<InventoryItem>();
        if (items != null && items.Length > 0)
        {
            foreach (InventoryItem item in items)
            {
                tempItems.Add(item);
            }
        }

        if (tempItems.Count <= 0)
        {
            Inventory.instance.ShowInventoryMessage(hasNoItemsString);
        }

        itemSpriteRenderer.sprite = null;
        items = null;
        return tempItems;
    }
}