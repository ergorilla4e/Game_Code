using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.Arm;

public class Movements : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [SerializeField] private Transform inventoryParent; 

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    private void OnMovement(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float length = direction.magnitude;

            if (length > 1)
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

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    private void Update()
    {
        UpdateUIInventory();    
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(h, v);

        OnMovement(direction);

        this.rb.MovePosition(this.rb.position + direction * (Time.deltaTime * this.speed));

    }

    public void UpdateUIInventory()
    {
        for (int i = 0; i < 3; i++)
        {
            Transform slotTransform = inventoryParent.GetChild(i);

            if (i < Inventory.Instance.items.Count && Inventory.Instance.items[i] != null)
            {
                // Controlla se l'oggetto UI è già istanziato
                if (slotTransform.childCount == 0)
                {
                    GameObject itemPrefab = Inventory.Instance.FindItemPrefabByName(Inventory.Instance.items[i].name);

                    if (itemPrefab != null)
                    {
                        Instantiate(itemPrefab, slotTransform.position, Quaternion.identity, slotTransform);
                    }
                }
            }
            else
            {
                if (slotTransform.childCount > 0)
                {
                    Destroy(slotTransform.GetChild(0).gameObject);
                }
            }
        }
    }

}
