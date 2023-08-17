using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Animator animator;
   
    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void OnMovement(Vector2 direction)
    {
         if(direction != Vector2.zero)
        {
            float length = direction.magnitude;

            if(length > 1 ) 
            {
                direction /= length;
            }

            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2 (h, v);

        OnMovement(direction);

        this.rb.MovePosition(this.rb.position + direction * (Time.deltaTime * this.speed));
       
    }

}
