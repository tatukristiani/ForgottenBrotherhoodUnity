using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

   

    private Button storyButton;
    private Button infinityButton;
    private Button quitButton;
    private Button muteButton;

    private Color muteColor;


    //We find all button objects and make eventlistener for them.
    public void Start()
    {
        storyButton = GameObject.Find("StoryButton").GetComponent<Button>();
        infinityButton = GameObject.Find("InfinityButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        muteButton = GameObject.Find("MuteButton").GetComponent<Button>();
        muteColor = muteButton.GetComponent<Image>().color; //Gets original color of mutebutton for later use.

        storyButton.onClick.AddListener(() => ButtonClicked(storyButton));
        infinityButton.onClick.AddListener(() => ButtonClicked(infinityButton));
        quitButton.onClick.AddListener(() => ButtonClicked(quitButton));
        muteButton.onClick.AddListener(() => ButtonClicked(muteButton));

        //Audiomanager starts playing menu music and stops other musics.
        AudioManager.instance.Play("MainMenuMusic");
        AudioManager.instance.Stop("InfinityMusic");
        AudioManager.instance.Stop("StoryMusic");


        //Here we check if sound is muted, we want the mute button to be red when coming from another scene.
        if (AudioManager.instance.isMuted)
        {
            muteButton.GetComponent<Image>().color = Color.red;
        }

    }

  

    //When a button from main menu is clicked, this function tells what scene to load. 
    //Mutebutton is an exeption. It handles the volume of music.
    private void ButtonClicked(Button button)
    {
       if(button == storyButton)
        {
            SceneManager.LoadScene("StoryScene");    

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
       
       if(button == muteButton)
        {
            //if sound is not muted -> change color and mute.
            if(!AudioManager.instance.isMuted)
            {
                muteButton.GetComponent<Image>().color = Color.red;
                AudioManager.instance.MuteMusic();
            }

            //if sound is muted -> change color and unmute.
            else
            {
                muteButton.GetComponent<Image>().color = muteColor;
                AudioManager.instance.UnMuteMusic();
            }
            
        }
    }  
}
