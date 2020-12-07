using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Core.Player
{
    [CreateAssetMenu(fileName = "PlayerAttributes", menuName = "Create Player Attributes")]
    public class PlayerAttributesSO : ScriptableObject
    {
        [SerializeField] private float speed;
        public float Speed { get => speed; }

        [SerializeField] private float climbingSpeedMultiplier;
        public float ClimbingSpeedMultiplier { get => climbingSpeedMultiplier; }

        [SerializeField] private float movementSmoothing;
        public float MovementSmoothing { get => movementSmoothing; }

        [SerializeField] private float jumpForce;
        public float JumpForce { get => jumpForce; }

        [SerializeField] private bool airControl = true;
        public bool AirControl { get => airControl; }

        [SerializeField] private float climbingSpeed = 10;
        public float ClimbingSpeed { get => climbingSpeed; }

        [SerializeField] private float jumpFromLadderForce = 1000f;
        public float JumpFromLadderForce { get => jumpFromLadderForce; }
    }
}