using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.Play("DestroyedBullet");
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        if(collision.collider.name!="Player" && collision.collider.name !="GroundCheck")
        {
            if(collision.collider.name.StartsWith("enemy")||collision.collider.name.StartsWith("HeadCollider"))
            {
                
                Destroy(collision.gameObject);
            }

            if (collision.collider.name.StartsWith("Boss"))
            {
                GameObject.Find("Boss").GetComponent<BossController>().BulletAttack();
            }
            StartCoroutine(BulletDestroy());
        }
    }

    IEnumerator BulletDestroy()
    {
        yield return new WaitForSeconds(.17f);
        Destroy(gameObject);
    }
}
