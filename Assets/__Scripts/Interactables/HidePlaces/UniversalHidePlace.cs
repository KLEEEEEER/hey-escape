using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Interactables.HidePlaces
{
    public class UniversalHidePlace : MonoBehaviour, IHidePlace
    {
        [SerializeField] private HidePlaceInfoSO hidePlaceInfoSO;
        [SerializeField] private bool isOpened = true;

        public HidePlaceInfoSO GetHidePlaceInfo()
        {
            hidePlaceInfoSO.transform = transform.position;
            return hidePlaceInfoSO;
        }

        public bool IsAccessible()
        {
            return isOpened;
        }

        public void OnHide(PlayerFSM player)
        {

        }

        public void OnUnhide(PlayerFSM player)
        {

        }
    }
}