using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Instruction()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
