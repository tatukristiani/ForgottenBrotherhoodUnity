using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//This class handles the onClick events of GameOverScreen in infinitymode and acts accordingly.
public class GameOverUI : MonoBehaviour
{
    private Button retryButton;
    private Button returnButton;
    public Text lastedRoundsText;

    private static int infinityRound;
    private int resetStory = -1;
    private int resetInfinity = 0;


    private void Start()
    {

        
        lastedRoundsText = GameObject.Find("LastedRoundsText").GetComponent<Text>();

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene"))
        { 
            infinityRound = WaveSpawner.fixedWaveNumber;

            if (infinityRound == 1)
            {
                lastedRoundsText.text = "You lasted " + infinityRound + " round.";

            }
            else
            {
                lastedRoundsText.text = "You lasted " + infinityRound + " rounds.";
            }
        
        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StoryScene"))
        {
            lastedRoundsText.text = "YOU DIED";
        }
       
        

       
        
    
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        returnButton = GameObject.Find("ReturnToMainMenuButton").GetComponent<Button>();

        retryButton.onClick.AddListener(() => ButtonClicked(retryButton));
        returnButton.onClick.AddListener(() => ButtonClicked(returnButton));
    }


    private void ButtonClicked(Button button)
    {
        if (button == retryButton)
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene"))
            {
                WaveSpawner.fixedWaveNumber = resetInfinity;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                GameManager.level = resetStory + 1;
                SceneManager.LoadScene("StoryScene");
            }
            

        }
        else if (button == returnButton)
        {
            WaveSpawner.fixedWaveNumber = resetInfinity;
            SceneManager.LoadScene("MenuScene");
            GameManager.level = resetStory;

        }
    }
}
