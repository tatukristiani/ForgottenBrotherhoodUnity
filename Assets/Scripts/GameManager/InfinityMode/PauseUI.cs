using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Class to handle Pause Interface when esc button is clicked.
//infinity and story modes levels are allso affected when pressing certain button, since they save their last numbers.
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

            //if we press menubutton in infinityscene, infinity scenes display number is resetted to 0.
            //And storys level is set to -1 since 0 will go straight to level with enemies.
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene"))
            {
                WaveSpawner.fixedWaveNumber = resetInfinity;
                GameManager.level = resetStory;
            }
            //if we press menubutton on storyscene we want to reset the story so it wont skip levels when coming straight back from main menu.
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
