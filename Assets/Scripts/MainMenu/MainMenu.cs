using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

   

    private Button storyButton;
    private Button infinityButton;
    private Button quitButton;

    //We find all button objects and make eventlistener for them.
    public void Start()
    {
        
        storyButton = GameObject.Find("StoryButton").GetComponent<Button>();
        infinityButton = GameObject.Find("InfinityButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

        storyButton.onClick.AddListener(() => ButtonClicked(storyButton));
        infinityButton.onClick.AddListener(() => ButtonClicked(infinityButton));
        quitButton.onClick.AddListener(() => ButtonClicked(quitButton));

        AudioManager.instance.Stop("InfinityMusic");
        AudioManager.instance.Play("MainMenuMusic");

    }

  

    //When a button from main menu is clicked, this function tells what scene to load.
    private void ButtonClicked(Button button)
    {
       if(button == storyButton)
        {
            SceneManager.LoadScene("Level1");    

        }
        else if(button == infinityButton)
        {
            SceneManager.LoadScene("InfinityScene");
            
        }
       else if(button == quitButton)
        {
            Debug.Log("Quitting game now!");
            Application.Quit();
        }        
    }  
}
