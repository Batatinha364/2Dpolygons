using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    float textDelay;
    private TextMeshProUGUI textDisplay;

    void Start() 
    {
        textDelay = 0;
        textDisplay = FindObjectOfType<TextMeshProUGUI>();
    }

    public void OnExit() 
    {
        Application.Quit();
    }

    void Update()
    {

        if (GameObject.Find("MainMenu")) 
        {
            textDelay += Time.deltaTime;

            if (textDelay >= .5f) 
            {
                textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, 0);

                if (textDelay >= 1f) 
                {
                    textDisplay.color = new Color(textDisplay.color.r, textDisplay.color.g, textDisplay.color.b, 1);
                    textDelay = 0;
                }
            }

            if (Input.GetButton("Submit"))
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
