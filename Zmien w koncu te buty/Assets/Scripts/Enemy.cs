using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float characterVelocity = 1f;

    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private int direction = -1;
    
    void Start()
    {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector()
    {
        float beforeDirectionX = movementDirection.x;

        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0).normalized;
        movementPerSecond = movementDirection * characterVelocity;

        if (beforeDirectionX != movementDirection.x)
        {
            if(transform.localScale.x>0 && movementDirection.x>0 || transform.localScale.x<0 && movementDirection.x<0)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                direction *= -1;
            }
        }
    }

    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
            
            GameObject.Find("Ninja").GetComponent<Animator>().Play("CharacterDie");
            StartCoroutine(ExampleCoroutine());
            GameObject.Find("Background").GetComponent<UIController>().GameOver();
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(GameObject.Find("Player"));
    }
}
