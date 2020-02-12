using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour {

    public Animator bossAnim;
    public Transform player;
    public Image healthBar;
    public GameObject healthBarMain;
    public GameObject weaponMain;
    public GameObject tryAgain;

    public float health = 100;
    public bool isAlive = true;

    private float offset = 5f;
    private float speed = -7f;
    private bool permission = true;
    private bool rushing = false;
    private int direction = 1;

    private void Start()
    {
        InvokeRepeating("ChooseAction", 3, 2);
    }

    private void Update()
    {
        WatchPlayer();
        if (rushing)
        {
            transform.position = new Vector2((transform.position.x + (speed * Time.deltaTime)*direction), transform.position.y);
        }
        healthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            healthBarMain.SetActive(false);
            weaponMain.SetActive(false);
            isAlive = false;

            GameObject.Find("Ninja").GetComponent<Animator>().Play("CharacterWin");
            Destroy(GameObject.Find("Player").GetComponent<PlayerMovement>());

            StartCoroutine(ExampleCoroutine("BossDie"));
            UiSnapping.DrawTextForever("Gratulacje! Przezwyciężyłeś szatniarki! Odważysz się przyjść w brudnych butach jeszcze raz?");
            tryAgain.SetActive(true);
        }

    }

    public void WatchPlayer()
    {
        float posX = transform.position.x;
        if (player.position.x > posX + (offset*transform.localScale.x))
        {
            if(transform.localScale.x>0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                direction = -1;
            }
        }
        else
        {
            if(transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                direction = 1;
            }
        }
    }

    public void BossAttack()
    {
        permission = false;
        BossPreparation();
        StartCoroutine(ExampleCoroutine("BossAttack"));
    }

    public void BossPreparation()
    {
        bossAnim.Play("BossHurt");
    }

    public void BossRush()
    {
        permission = false;
        BossPreparation();
        StartCoroutine(ExampleCoroutine("BossRush"));
    }

    public void BossIdle()
    {
        bossAnim.Play("BossAnim");
    }

    public void ChooseAction()
    {
       int rand = Random.Range(2, 4);

       if(permission)
        {
            switch (rand)
            {
                case 1:
                    {
                        BossIdle();
                        break;
                    }
                case 2:
                    {
                        BossRush();
                        break;
                    }
                case 3:
                    {
                        BossAttack();
                        break;
                    }
            }
        }

    }

    IEnumerator ExampleCoroutine(string animName)
    {
        if (animName == "BossDie")
        {
            bossAnim.Play(animName);
        }

        yield return new WaitForSeconds(1f);

        if (animName == "BossDie")
        {
            Destroy(gameObject);
        }

        bossAnim.Play(animName);

        if (animName == "BossAttack")
        {
            StartCoroutine(animLong(2.17f));
        }
        if (animName == "BossRush")
        {
            rushing = true;
            StartCoroutine(animLong(1f));
        }
    }

    IEnumerator animLong(float timeLong)
    {
        yield return new WaitForSeconds(timeLong);
        permission = true;
        rushing = false;
    }

    public void BulletAttack()
    {
        health -= 10;
    }
}
