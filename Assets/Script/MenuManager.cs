using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Connection");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

