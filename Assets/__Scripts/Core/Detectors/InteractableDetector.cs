using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class InteractableDetector : Detector<IInteractable>
    {
        public override void CheckCollidersInArray(Collider2D[] colliders)
        {
            base.CheckCollidersInArray(colliders);
        }

        public override bool InteractWithFoundColliders()
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