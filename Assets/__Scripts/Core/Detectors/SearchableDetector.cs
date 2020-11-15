using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class SearchableDetector : Detector<ISearchable>
    {
        Inventory inventory;
        public SearchableDetector(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void CheckCollidersInArray(Collider2D[] colliders)
        {
            base.CheckCollidersInArray(colliders);
        }

        public override bool InteractWithFoundColliders(Action onInteractionAction = null)
        {
            Debug.Log("Interacted with searchable");

            bool interacted = false;
            if (detectedColliders.Count > 0)
            {
                //animator.SetTrigger("Search");

                foreach (ISearchable searchable in detectedColliders)
                {
                    List<InventoryItem> enemyItems = searchable.Search();
                    if (enemyItems.Count > 0)
                    {
                        foreach (InventoryItem item in enemyItems)
                        {
                            inventory.AddItem(item);
                        }
                    }
                }
                interacted = true;
                onInteractionAction?.Invoke();
            }
            return interacted;
        }
    }
}