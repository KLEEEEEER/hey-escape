﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Detectors
{
    public class HidePlaceDetector : Detector<IHidePlace>
    {
        public override void CheckCollidersInArray(Collider2D[] colliders)
        {
            base.CheckCollidersInArray(colliders);
        }

        public override bool InteractWithFoundColliders(Action onInteractionAction = null)
        {
            bool interacted = false;
            if (detectedColliders.Count > 0)
            {


                onInteractionAction?.Invoke();
            }
            return interacted;
        }
    }
}