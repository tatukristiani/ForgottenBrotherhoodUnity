using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Class to handle Pause Interface when esc button is clicked.
public class PauseUI : MonoBehaviour
{

    private Button continueButton;
    private Button menuButton;
    private Button muteButton;

    private Color originalColor;

    private int resetStory = -1;
    private int resetInfinity = 0;


    //We find all buttons and save the original color of mutebutton.
    //Add onClick eventlistener for all buttons.
    private void Start()
    {
        
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        muteButton = GameObject.Find("PauseMuteButton").GetComponent<Button>();
        originalColor = muteButton.GetComponent<Image>().color;

        continueButton.onClick.AddListener(() => ButtonClicked(continueButton));
        menuButton.onClick.AddListener(() => ButtonClicked(menuButton));
        muteButton.onClick.AddListener(() => ButtonClicked(muteButton));


        //Check if audio is muted, if it is -> change mute button color.
        if(AudioManager.instance.isMuted)
        {
            muteButton.GetComponent<Image>().color = Color.red;
        }
        
    }


    
    private void ButtonClicked(Button button)
    {
        //simply just continues the game. More explaining in GameMaster class.
        if(button == continueButton)
        {
            GameMaster.gm.GameContinue();
            
        }

        //Transfers scene to MainMenu. More explaining in GameMaster class.
        else if(button == menuButton)
        {
            GameMaster.gm.GameContinue();
            SceneManager.LoadScene("MenuScene");
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene"))
            {
                WaveSpawner.fixedWaveNumber = resetInfinity;
                GameManager.level = resetStory;
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StoryScene"))
            {
                GameManager.level = resetStory;
            }
        }


        //Mutes/unmutes the audio depending on the Audiomanagers variable isMuted.
        //Sets the color.
        if(button == muteButton)
        {
            if(!AudioManager.instance.isMuted)
            {
                AudioManager.instance.MuteMusic();
                muteButton.GetComponent<Image>().color = Color.red;

            }
            else
            {
                AudioManager.instance.UnMuteMusic();
                muteButton.GetComponent<Image>().color = originalColor;
            }
            
        }
    }
}
