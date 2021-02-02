using HeyEscape.Core.Detectors;
using HeyEscape.Core.Interfaces;
using HeyEscape.Core.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Detectors
{
    public class KillableDetector : Detector<IKillable>
    {
        public override void CheckCollidersInArray(Collider2D[] colliders, int amount)
        {
            detectedColliders.Clear();

            if (colliders.Length == 0) return;

            for (int i = 0; i < amount; i++)
            {
                IKillable foundCollider = colliders[i].GetComponent<IKillable>();
                if (foundCollider != null)
                {
                    Enemy.Enemy enemy = colliders[i].GetComponent<Enemy.Enemy>();
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