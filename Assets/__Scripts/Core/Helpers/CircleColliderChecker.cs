using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Helpers
{
    public class CircleColliderChecker : MonoBehaviour
    {
        [SerializeField] Transform checkPoint;
        [SerializeField] float radius;
        [SerializeField] LayerMask checkLayers;
        Collider2D[] colliders;

        bool isTrue = false;

        public bool Check()
        {
            colliders = Physics2D.OverlapCircleAll(checkPoint.position, radius, checkLayers);
            bool foundGround = false;
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    foundGround = true;
                    break;
                }
            }
            isTrue = foundGround;
            return foundGround;
        }

        public bool IsTrue => isTrue;
    }
}