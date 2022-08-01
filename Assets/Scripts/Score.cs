using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreUI;
    private int score;
    private bool playerDied;
    float delay;

    void Start()
    {
        score = 0;
        playerDied = false;
        scoreUI = FindObjectOfType<TextMeshProUGUI>();
        scoreUI.text = "" + score;
        delay = 0;
    }

    void ScorePointsAdd(int pointsToChange) 
    {
        score += pointsToChange;
        scoreUI.text = "" + score;
    }

    void ScorePointsDecr()
    {
        if(score <= 15) 
        {
            score -= 4;
            scoreUI.text = "" + score;
        }
        else
        {
            score -= (int)(0.1 * score);
            scoreUI.text = "" + score;
        }

        if (score <= 2) 
        {
            scoreUI.text = "Game Over";
            Destroy(GameObject.FindWithTag("Player"));
            Destroy(GameObject.FindWithTag("SpawnerX"));
            Destroy(GameObject.FindWithTag("SpawnerY"));

            playerDied = true;
        }
    }

    void OnExit() 
    {
        Debug.Log("Sair do Jogo!");
        Application.Quit();
    }

    void ZeroScore() 
    {
        score = 0;
        scoreUI.text = "" + score;
    }

    void Leave() 
    {
        SceneManager.LoadScene("MainScene");
    }

    void LoadDemo() 
    {
        if (score >= 1000 && Input.GetKey(KeyCode.F4))
            SceneManager.LoadScene("Demo");
    }

    void Update()
    {

        if (playerDied) 
        {
            delay += Time.deltaTime;

            if (delay >= 6)
                scoreUI.text = "Press any key...";

            if (Input.anyKey && delay >= 5) 
            {
                SceneManager.LoadScene("MainScene");
            }
        }

        if (Input.GetKey(KeyCode.Escape))
            OnExit();

        if(playerDied == false) 
        {
            if (Input.GetKey(KeyCode.F1))
                ZeroScore();
            else if (Input.GetKey(KeyCode.F2))
                Leave();
        }

        LoadDemo();
    }
}
