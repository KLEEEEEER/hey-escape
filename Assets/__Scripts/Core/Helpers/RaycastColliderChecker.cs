using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Helpers
{
    public class RaycastColliderChecker : MonoBehaviour
    {
        [SerializeField] Transform raycastStartPoint;
        [SerializeField] Transform raycastEndPoint;
        [SerializeField] LayerMask checkLayers;
        RaycastHit2D[] hits = new RaycastHit2D[10];

        bool isTrue = false;

        public bool Check()
        {
            int amount = Physics2D.RaycastNonAlloc(
                raycastStartPoint.position,
                (raycastEndPoint.position - raycastStartPoint.position).normalized,
                hits,
                Vector2.Distance(raycastStartPoint.position, raycastEndPoint.position),
                checkLayers
            );
            isTrue = amount > 0;

            return isTrue;
        }

        public bool IsTrue => isTrue;
    }
}