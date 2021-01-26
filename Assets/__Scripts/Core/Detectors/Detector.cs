using HeyEscape.Core.Player.FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class Detector<T>
    {
        protected List<T> detectedColliders = new List<T>();
        protected PlayerFSM player;

        public Detector(PlayerFSM player = null)
        {
            this.player = player;
        }

        public virtual void CheckCollidersInArray(Collider2D[] colliders, int amount)
        {
            detectedColliders.Clear();

            if (colliders.Length == 0) return;

            //foreach (Collider2D collider in colliders)
            for (int i = 0; i < amount; i++)
            {
                //if (colliders[i] == null) continue;
                T foundCollider = colliders[i].GetComponent<T>();
                if (foundCollider != null)
                {
                    detectedColliders.Add(foundCollider);
                }
            }
        }

        public virtual bool InteractWithFoundColliders(Action onInteractionAction = null)
        {
            onInteractionAction();
            return false;
        }

        public virtual T GetFirstFoundObject()
        {
            return detectedColliders[0];
        }

        public int GetDetectedCollidersCount()
        {
            return detectedColliders.Count;
        }
    }
}