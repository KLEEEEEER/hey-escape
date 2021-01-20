using HeyEscape.Core.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Interactables.HidePlaces
{
    [CreateAssetMenu(fileName = "HidePlaceInfo", menuName = "Create new HidePlace info")]
    public class HidePlaceInfoSO : ScriptableObject
    {
        public Vector3 transform = Vector3.zero;
        public Vector3 scale = new Vector3(1f, 1f, 1f);
        public Color color;
        public bool DiactivatePlayerOnHide = false;
        public VisibilityState.State visibilityState = VisibilityState.State.Hidden;
        public PlayerLightVision.VisionState lightVisionState = PlayerLightVision.VisionState.Full;

        [SerializeField] private PlayerHidingSpriteType playerHidingSprite = PlayerHidingSpriteType.CenterStand;

        [SerializeField] public bool hidePlayerSpriteAfterTime = false;
        [SerializeField] public float delayBeforeHiddingPlayerSprite = 0f;
        public PlayerHidingSpriteType PlayerHidingSprite { get => playerHidingSprite; }
    }
}