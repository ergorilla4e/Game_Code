using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cliente_Animation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 lastPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastPosition = rb.position;
    }

    private void OnMovement(Vector2 movementDirection)
    {
        if (movementDirection != Vector2.zero)
        {
            float length = movementDirection.magnitude;

            if (length > 1)
            {
                movementDirection /= length;
            }

            animator.SetFloat("X", movementDirection.x);
            animator.SetFloat("Y", movementDirection.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;
        Vector2 movementDirection = currentPosition - lastPosition;

        OnMovement(movementDirection);

        lastPosition = currentPosition;
    }
}
