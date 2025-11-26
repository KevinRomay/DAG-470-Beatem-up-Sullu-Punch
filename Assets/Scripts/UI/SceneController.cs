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

    public void GoToPruebaTransiciones()
    {
        SceneManager.LoadScene("PruebaTransiciones");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
