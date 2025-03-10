using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles
{
    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;

        public float speed = 5f;
        public float gravity = -15f;

        Vector3 velocity;

        bool isGrounded;

        // Update is called once per frame
        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Create the movement vector
            Vector3 move = transform.right * x + transform.forward * z;

            // Normalize the movement vector to prevent faster diagonal movement
            if (move.magnitude > 1f)
            {
                move.Normalize();
            }

            // Apply movement
            controller.Move(move * speed * Time.deltaTime);

            // Apply gravity
            velocity.y += gravity * Time.deltaTime;

            // Apply gravity movement
            controller.Move(velocity * Time.deltaTime);
        }
    }
}