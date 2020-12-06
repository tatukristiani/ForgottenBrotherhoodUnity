using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Simple game won screen and button to return.
public class GameWon : MonoBehaviour
{

    private Button menuButton;
    private int resetStory = -1;

    private void Start()
    {
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();

        menuButton.onClick.AddListener(() => ButtonClicked(menuButton));
      
    }

    private void ButtonClicked(Button button)
    {
        SceneManager.LoadScene("MenuScene");
        GameManager.level = resetStory;
    }
}
