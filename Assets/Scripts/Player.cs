using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public float speed = 100;
    public LayerMask villagerLayer;
    Vector2 movementDirection;
    Rigidbody2D rigidbody;
    Vector2 hitOrigin;
    Collider2D[] colliders;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.NextLevel();
        }

        if (Input.GetMouseButtonDown(0))
        {
            hitOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            colliders = Physics2D.OverlapCircleAll(hitOrigin, 0.25f, villagerLayer);

            if (colliders != null)
            {
                
                Villagers enemy = gameManager.enemyVillager;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].GetComponent<Villagers>() == enemy)
                    {
                        gameManager.NextLevel();
                        print("Hit Enemy");
                    }
                    else
                    {
                        gameManager.IncrementStrikes();
                        Destroy(colliders[i].gameObject);
                    }
                }
            }         
           
        }
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementDirection = movementDirection.normalized;
    }

    void FixedUpdate ()
    {
        rigidbody.velocity = (movementDirection * speed * Time.fixedDeltaTime);
    }
}
