using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void GoToCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
