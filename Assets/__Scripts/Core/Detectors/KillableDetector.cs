using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class KillableDetector : Detector<IKillable>
    {
        public override void CheckCollidersInArray(Collider2D[] colliders, int amount)
        {
            detectedColliders.Clear();

            if (colliders.Length == 0) return;

            //foreach (Collider2D collider in colliders)
            for (int i = 0; i < amount; i++)
            {
                //if (collider == null) continue;
                IKillable foundCollider = colliders[i].GetComponent<IKillable>();
                if (foundCollider != null)
                {
                    Enemy enemy = colliders[i].GetComponent<Enemy>();
                    if (enemy != null && !enemy.IsDead())
                    {
                        detectedColliders.Add(foundCollider);
                    }
                }
            }
        }

        public override bool InteractWithFoundColliders(Action onInteractionAction = null)
        {
            bool interacted = false;
            if (detectedColliders.Count > 0)
            {
                foreach (IKillable interactable in detectedColliders)
                {
                    interactable.Kill();
                    interacted = true;
                }
                onInteractionAction?.Invoke();
            }
            return interacted;
        }
    }
}