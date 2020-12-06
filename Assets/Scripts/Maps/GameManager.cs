using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;
    public BoardManager boardScript; 
    public static int level = 0;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
  
        boardScript = GetComponent<BoardManager>();

        InitGame();
    }


    void OnLevelWasLoaded(int index) 
    {

        InitGame();
        level++;

    }


    void InitGame()
    {

        boardScript.SetupScene(level);
        
    }
}