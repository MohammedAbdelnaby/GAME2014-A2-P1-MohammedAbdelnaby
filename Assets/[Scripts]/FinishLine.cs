using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            soundManager.PlaySoundFX(Sound.FINISH, Channel.PLAYER_SOUND_FINISH);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.gameObject.GetComponent<Player>().enabled = false;
            StartCoroutine("Win");
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2.0f);
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            SceneManager.LoadScene("Level_2");
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
    }
}
