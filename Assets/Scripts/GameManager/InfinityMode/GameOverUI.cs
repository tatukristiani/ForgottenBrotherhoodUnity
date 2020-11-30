using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//This class handles the onClick events of GameOverScreen in infinitymode and acts accordingly.
public class GameOverUI : MonoBehaviour
{
    private Button retryButton;
    private Button returnButton;


    private void Start()
    {
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        returnButton = GameObject.Find("ReturnToMainMenuButton").GetComponent<Button>();

        retryButton.onClick.AddListener(() => ButtonClicked(retryButton));
        returnButton.onClick.AddListener(() => ButtonClicked(returnButton));
    }

    private void ButtonClicked(Button button)
    {
        if (button == retryButton)
        {
            Debug.Log("Retrying");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else if (button == returnButton)
        {
            Debug.Log("Returned to main menu");
            SceneManager.LoadScene("MenuScene");

        }
    }
}
