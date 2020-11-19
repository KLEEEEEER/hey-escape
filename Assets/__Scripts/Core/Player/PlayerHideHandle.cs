using Core.Detectors;
using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.HidePlaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player
{
    public class PlayerHideHandle : MonoBehaviour
    {
        [SerializeField] SpriteRenderer playerRenderer;
        [SerializeField] PlayerFSM playerFSM;
        [SerializeField] VisibilityState visibilityState;
        [SerializeField] private float SearchHidePlaceRadius = 1f;
        private bool isHidden = false;
        public bool IsHidden { get => isHidden; private set => isHidden = value; }

        private Vector3 initPosition;
        private Vector3 initScale;
        private Color initColor;

        public void Hide(HidePlaceInfoSO hidePlaceInfo)
        {
            SaveInitialPlayerParams();

            playerFSM.TransitionToState(playerFSM.DisableState);
            transform.position = hidePlaceInfo.transform;
            transform.localScale = hidePlaceInfo.scale;
            playerRenderer.color = hidePlaceInfo.color;
            visibilityState.SetVisibilityState(hidePlaceInfo.visibilityState);

            IsHidden = true;
        }

        public void Unhide()
        {
            playerFSM.TransitionToState(playerFSM.IdleState);
            transform.position = initPosition;
            transform.localScale = initScale;
            playerRenderer.color = initColor;
            visibilityState.SetVisibilityState(VisibilityState.State.Visible);

            IsHidden = false;
        }

        private void SaveInitialPlayerParams()
        {
            initPosition = transform.position;
            initScale = transform.localScale;
            initColor = playerRenderer.color;
        }
    }
}