using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampAnimations : MonoBehaviour {
    public RuntimeAnimatorController controller;
    public GameObject gb;

    bool onFocus = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            gb.gameObject.GetComponent<Animator>().runtimeAnimatorController = controller;
            gb.gameObject.GetComponent<Animator>().Play("LampHit");
            onFocus = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            StartCoroutine(ExampleCoroutine());
            onFocus = false;
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);
        if(!onFocus)
        gb.gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
    }
}
