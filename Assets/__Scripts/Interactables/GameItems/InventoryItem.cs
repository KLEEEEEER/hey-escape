using System;
using UnityEngine;
using UnityEngine.Localization;

namespace HeyEscape.Interactables.GameItems
{
    public class InventoryItem : MonoBehaviour
    {
        public LocalizedString nameLocalized;
        [HideInInspector] public string nameString;
        public Sprite Icon;

        public void Awake()
        {
            nameLocalized.RegisterChangeHandler(UpdateString);
        }

        public void UpdateString(string s) { nameString = s; }
    }
}