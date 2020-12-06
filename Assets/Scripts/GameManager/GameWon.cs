using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{

    private Button menuButton;

    private void Start()
    {
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();

        menuButton.onClick.AddListener(() => SceneManager.LoadScene("MenuScene"));
      
    }
}
