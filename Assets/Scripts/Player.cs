using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public float speed = 100;
    public LayerMask villagerLayer;

    public AudioClip correctSelectionSound;
    public AudioClip incorrectSelectionSound;
    Vector2 movementDirection;
    Rigidbody2D rigidbody;
    Vector2 hitOrigin;
    Collider2D[] colliders;
    GameManager gameManager;
    AudioSource audioSource;

    void Start()
    {
        gameManager = GameManager.instance;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        audioSource = GetComponent<AudioSource>();

        if (gameManager.level > 1)
        {
            audioSource.clip = correctSelectionSound;
            audioSource.Play();
        }
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

                    }
                    else
                    {
                        audioSource.clip = incorrectSelectionSound;
                        audioSource.Play();
                        gameManager.IncrementStrikes();
                        colliders[i].GetComponent<Villagers>().BlowUp();
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
