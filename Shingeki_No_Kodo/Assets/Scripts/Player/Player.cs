using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private GameObject CanvasWindow;

    private Rigidbody2D _rb;
    private Animator _animator;

    private void Awake()
    {
        this._rb = GetComponent<Rigidbody2D>();
        this._animator = GetComponent<Animator>();
    }

    //Funzione che prende in input il movimento del player e gestisce quale animazione mandare a schermo in base alla direzione
    private void OnMovement(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float length = direction.magnitude;

            if (length > 1)
            {
                direction /= length;
            }

            _animator.SetFloat("X", direction.x);
            _animator.SetFloat("Y", direction.y);

            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(h, v);

        OnMovement(direction);

        this._rb.MovePosition(this._rb.position + direction * (Time.deltaTime * this.speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interazione_Finestra"))
        {
            CanvasWindow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CanvasWindow.SetActive(false);
    }
   
}



