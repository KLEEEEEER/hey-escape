﻿using UnityEngine;

namespace HeyEscape.Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float speed = 7f;
        [SerializeField] float verticalSpeed = 5f;
        Rigidbody2D rb;
        private bool isEnabled = true;
        public bool IsEnabled { get => isEnabled; }

        private bool isLookingRight = true;
        public bool IsLookingRight { get => isLookingRight; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 horVer, float multiplier = 1f)
        {
            if (!isEnabled) return;
            CheckRotation(horVer.x);
            rb.velocity = (horVer * speed) * multiplier;
        }

        public void MoveHorizontally(float horizontal, float multiplier = 1f)
        {
            if (!isEnabled) return;
            CheckRotation(horizontal);
            rb.velocity = new Vector2((horizontal * speed * multiplier), rb.velocity.y);
            //rb.AddForce(new Vector2((horizontal * speed * multiplier), 0f), ForceMode2D.Force);
        }

        public void MoveVertically(float vertical, float horizontal, float multiplier = 1f)
        {
            if (!isEnabled) return;
            CheckRotation(horizontal);
            rb.velocity = (new Vector2(0f, vertical) * verticalSpeed) * multiplier;
        }

        public void SetEnabled(bool enabled)
        {
            isEnabled = enabled;
        }

        public void CheckRotation(float horizontal)
        {
            if (isLookingRight && horizontal < 0)
            {
                Flip();
            }
            else if (!isLookingRight && horizontal > 0)
            {
                Flip();
            }
        }
        public void Flip()
        {
            isLookingRight = !isLookingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}