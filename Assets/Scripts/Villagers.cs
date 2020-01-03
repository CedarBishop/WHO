using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villagers : MonoBehaviour
{
    public float minMoveSpeed;
    public float maxMoveSpeed;
    public float maxWaitTime;
    public float minWaitTime;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public Sprite[] sprites;

    private Vector2 target;
    private float moveSpeed;
    private float waitTime;
    private bool canMove;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        moveSpeed = Random.Range(minMoveSpeed,maxMoveSpeed);
        canMove = true;
        target = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length > 0)
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.color = new Color(Random.Range(0.5f,1.0f),Random.Range(0.5f,1.0f),Random.Range(0.5f,1.0f),1.0f);
    }

    
    void Update()
    {
        if (canMove)
        {
            if (Vector2.Distance(transform.position,target) < Mathf.Epsilon)
            {
                canMove = false;
                waitTime = Random.Range(minWaitTime,maxWaitTime);
                StartCoroutine("Wait");
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
        }
    }

    IEnumerator Wait ()
    {
        target = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
        yield return new WaitForSeconds(waitTime);
        canMove = true;

    }

}
