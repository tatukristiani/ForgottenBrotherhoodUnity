using UnityEngine;
using UnityEngine.SceneManagement;


//This class handles destroying of enemy and player. + Setting up the pause screen and game over screen.
//Music starting and stopping added allso.
public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject pauseUI;

    [SerializeField]
    private GameObject gameWonUI;

    [SerializeField]
    private GameObject storyIntructionsUI;

    private bool isPaused;


    private void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }

       
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("InfinityScene"))
        {
            AudioManager.instance.Play("InfinityMusic");
            AudioManager.instance.Stop("MainMenuMusic");

        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StoryScene"))
        {
            AudioManager.instance.Play("StoryMusic");
            AudioManager.instance.Stop("MainMenuMusic");
        }
    }

    //We just check if player uses Escape key.
    //Check if on first level of story -> display instructions.
    private void Update()
    {
        CheckPlayerPause();
        CheckIfInstructionsNeeded();
    }

    //Destroys player gameobject after 2seconds and call GameOver() method.
    //Sets player to null so EnemyAttack won't have reference error.
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject, 2f);
        player = null;
        gm.GameOver();
    }
    
    //Destroys enemy object.
    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        
    }

    //Kills boss and displays winning screen.
    public static void KillBoss(Enemy enemy)
    {
        Destroy(enemy.gameObject, 2f);
        gm.GameWon();
    }

    //Starts the GameOverScreen when called.
    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        gameOverUI.SetActive(true);
    }

    //Start the gamewon screen.
    public void GameWon()
    {
        Debug.Log("GAME WON!");
        gameWonUI.SetActive(true);

    }


    //Stops time out of everything that uses time. Pauses music and sets the pause screen active.
    public void GamePaused()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseUI.SetActive(true);   
    }

    //Continue from pause screen, time is back to normal, audio back on.
    public void GameContinue()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);
         
    }


    //Method checks if the story is started from the beginning and displays the instructions. Disables them when second level starts.
    public void CheckIfInstructionsNeeded()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("StoryScene"))
        {
            if(GameManager.level == 1)
            {
                storyIntructionsUI.SetActive(true);
            }
            else if(GameManager.level == 2)
            {
                storyIntructionsUI.SetActive(false);
            }
        }
    }


    //Checks if player uses escape key and checks if the game is allready paused and acts accordinly.
    public void CheckPlayerPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = !isPaused;
                GamePaused();

            }
            else
            {
                isPaused = !isPaused;
                GameContinue();
            }
            
        }
    }

}
