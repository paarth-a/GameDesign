using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Player.PlayerType playerType;

    private bool hasSelectedType = false;

    public void PlayGame()
    {
        if (hasSelectedType)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void SelectArcher()
    {
        playerType = Player.PlayerType.ARCHER;
        hasSelectedType = true;
    }

    public void SelectKnight()
    {
        playerType = Player.PlayerType.KNIGHT;
        hasSelectedType = true;
    }

    public void SelectWizard()
    {
        playerType = Player.PlayerType.WIZARD;
        hasSelectedType = true;
    }
}