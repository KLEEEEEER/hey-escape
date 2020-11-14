using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class Detector<T>
    {
        protected List<T> detectedColliders = new List<T>();

        public virtual void CheckCollidersInArray(Collider2D[] colliders)
        {
            detectedColliders.Clear();

            if (colliders.Length == 0) return;

            foreach (Collider2D collider in colliders)
            {
                if (collider == null) continue;

                T foundCollider = collider.GetComponent<T>();
                if (foundCollider != null)
                {
                    detectedColliders.Add(foundCollider);
                }
            }
        }

        public virtual bool InteractWithFoundColliders()
        {
            Debug.Log("Interacting with founded colliders");
            return false;
        }
    }
}