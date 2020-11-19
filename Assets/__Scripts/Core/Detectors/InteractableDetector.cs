using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class InteractableDetector : Detector<IInteractable>
    {
        public override void CheckCollidersInArray(Collider2D[] colliders, int amount)
        {
            base.CheckCollidersInArray(colliders, amount);
        }

        public override bool InteractWithFoundColliders(Action onInteractionAction = null)
        {
            bool interacted = false;
            if (detectedColliders.Count > 0)
            {
                foreach (IInteractable interactable in detectedColliders)
                {
                    interactable.Interact();
                    interacted = true;
                }
            }
            return interacted;
        }
    }
}