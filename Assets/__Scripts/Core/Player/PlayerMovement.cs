using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float speed = 7f;
        [SerializeField] float verticalSpeed = 5f;
        Rigidbody2D rb;
        private bool isEnabled = false;
        public bool IsEnabled { get => isEnabled; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 horVer, float multiplier = 1f)
        {
            if (!isEnabled) return;
            rb.velocity = (horVer * speed) * multiplier;
        }

        public void MoveHorizontally(float horizontal, float multiplier = 1f)
        {
            if (!isEnabled) return;
            rb.velocity = (new Vector2(horizontal, rb.velocity.y) * speed) * multiplier;
        }

        public void MoveVertically(float vertical, float multiplier = 1f)
        {
            if (!isEnabled) return;
            rb.velocity = (new Vector2(0f, vertical) * verticalSpeed) * multiplier;
        }

        public void SetEnabled(bool enabled)
        {
            isEnabled = enabled;
        }
    }
}