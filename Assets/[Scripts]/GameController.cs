using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level_1":
                soundManager.PlaySoundFX(Sound.LEVEL_1, Channel.MUSIC);
                break;
            case "Level_2":
                soundManager.PlaySoundFX(Sound.LEVEL_2, Channel.MUSIC);
                break;
            case "MainMenu":
                soundManager.PlaySoundFX(Sound.MAINMENU_N_WIN, Channel.MUSIC);
                break;
            case "Win":
                soundManager.PlaySoundFX(Sound.MAINMENU_N_WIN, Channel.MUSIC);
                break;
            case "GameOver":
                soundManager.PlaySoundFX(Sound.LOST, Channel.MUSIC);
                break;
            case "Instruction":
                soundManager.PlaySoundFX(Sound.MAINMENU_N_WIN, Channel.MUSIC);
                break;
            default:
                break;
        }
    }
}
