using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDeath : MonoBehaviour {

    [SerializeField]
    public GameObject enemy;

    string footsColliderName = "GroundCheck";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == footsColliderName)
        {
            Death();
        }
    }

    public void Death()
    {
        enemy.GetComponent<Animator>().Play("SprzataczkaDie");
        enemy.GetComponent<BoxCollider2D>().enabled = false;
        enemy.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(enemy);
    }
}
