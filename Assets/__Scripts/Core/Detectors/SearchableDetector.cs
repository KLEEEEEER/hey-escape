using HeyEscape.Core.Detectors;
using HeyEscape.Core.Interfaces;
using HeyEscape.Core.Inventory;
using HeyEscape.Interactables.GameItems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Detectors
{
    public class SearchableDetector : Detector<ISearchable>
    {
        Inventory.Inventory inventory;
        public SearchableDetector(Inventory.Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void CheckCollidersInArray(Collider2D[] colliders, int amount)
        {
            base.CheckCollidersInArray(colliders, amount);
        }

        public override bool InteractWithFoundColliders(Action onInteractionAction = null)
        {
            bool interacted = false;
            if (detectedColliders.Count > 0)
            {
                bool searchHappened = false;
                foreach (ISearchable searchable in detectedColliders)
                {
                    if (searchable.IsSearched) continue;

                    List<InventoryItem> enemyItems = searchable.Search();
                    if (enemyItems.Count > 0)
                    {
                        foreach (InventoryItem item in enemyItems)
                        {
                            inventory.AddItem(item);
                        }
                        searchHappened = true;
                    }
                }
                interacted = true;
                if (searchHappened)
                    onInteractionAction?.Invoke();
            }
            return interacted;
        }
    }
}