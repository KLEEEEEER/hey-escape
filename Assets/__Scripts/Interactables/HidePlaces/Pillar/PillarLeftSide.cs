using HeyEscape.Core.Player;
using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace HeyEscape.Interactables.HidePlaces
{
    public class PillarLeftSide : MonoBehaviour, IHidePlace
    {
        [SerializeField] private Color HideColor;

        private bool isHidden = false;
        private Vector3 currentVelocity;
        [SerializeField] private float smoothTime;
        [SerializeField] private ShadowCaster2D shadowCaster;

        public bool IsAccessible()
        {
            return true;
        }

        [SerializeField] private HidePlaceInfoSO hidePlaceInfoSO;
        public HidePlaceInfoSO GetHidePlaceInfo()
        {
            hidePlaceInfoSO.transform = transform.position;
            return hidePlaceInfoSO;
        }

        public void OnHide(PlayerFSM player)
        {
            shadowCaster.gameObject.SetActive(true);
        }

        public void OnUnhide(PlayerFSM player)
        {
            shadowCaster.gameObject.SetActive(false);
        }
    }
}