using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int playerChoice = 0;

    public void PlayGame()
    {
        if (playerChoice != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SelectArcher()
    {
        playerChoice = 1;
    }

    public void SelectKnight()
    {
        playerChoice = 2;
    }

    public void SelectWizard()
    {
        playerChoice = 3;
    }
}