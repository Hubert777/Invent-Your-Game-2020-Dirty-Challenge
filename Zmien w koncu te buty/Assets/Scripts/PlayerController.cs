using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public Transform spawnBullet;
    public Transform player;
    public GameObject[] bullets;
    public GameObject boss;
    public GameObject ui1;
    public GameObject ui2;
    public Image BulletLoad;

    int side = 1;
    bool spawned = true;
    bool permission = false;
    
	void Start () {
        UiSnapping.DrawText("Naskocz na wszystkie szatniarki, aby móc uchować się przed ich gniewem!");
        BulletLoad.fillAmount = 1;
	}
	
	void Update () {
        Attack();
        CheckEnemies();
        LoadingBullet();
	}

    void Attack()
    {
        if (Input.GetKeyDown("e") && BulletLoad.fillAmount == 1 && spawned == false)
        {
            BulletLoad.fillAmount = 0;
            permission = true;

            LoadingBullet();

            GameObject bullet = Instantiate(bullets[1], spawnBullet.transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (player.transform.localScale.x < 0)
                side = -1;
            else
                side = 1;
            rb.AddForce(transform.right * 600f * side);

            StartCoroutine(BulletDestroy(bullet));
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    void CheckEnemies()
    {
        if (spawned)
        {
            GameObject gb = GameObject.FindGameObjectWithTag("enemy");

            if (gb == null)
            {
                SpawnBoss();
            }
        }
    }

    public void SpawnBoss()
    {
        Destroy(GameObject.Find("BossWall"));

        UiSnapping.DrawText("Nie ma tak łatwo kochanieńki... Pokonaj Strażniczkę Szatni!!!");

        boss.SetActive(true);
        spawned = false;
        ui1.SetActive(true);
        ui2.SetActive(true);
    }

    public void LoadingBullet()
    {
        if(permission==true)
        BulletLoad.fillAmount += 1.2f * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "szczota"&&GameObject.Find("Boss").GetComponent<BossController>().isAlive)
        {
            GameObject.Find("Ninja").GetComponent<Animator>().Play("CharacterDie");
            StartCoroutine(ExampleCoroutine());
            GameObject.Find("Background").GetComponent<UIController>().GameOver();
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
    }

    IEnumerator BulletDestroy(GameObject go)
    {
        yield return new WaitForSeconds(.75f);
        Destroy(go);
    }

}
