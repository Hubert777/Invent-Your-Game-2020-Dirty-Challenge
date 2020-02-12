using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    [SerializeField]
    public Text gameover;
    public Button tryAgain;

    public void GameOver()
    {
        gameover.enabled = true;
        tryAgain.enabled = true;
        tryAgain.GetComponentInChildren<Text>().enabled = true;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
