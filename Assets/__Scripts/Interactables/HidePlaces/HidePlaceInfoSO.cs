using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables.HidePlaces
{
    [CreateAssetMenu(fileName = "HidePlaceInfo", menuName = "Create new HidePlace info")]
    public class HidePlaceInfoSO : ScriptableObject
    {
        public Vector3 transform = Vector3.zero;
        public Vector3 scale = new Vector3(1f, 1f, 1f);
        public Color color;
    }
}