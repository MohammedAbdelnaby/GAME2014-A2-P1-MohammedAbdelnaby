using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public Animator animator;
    private SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            soundManager.PlaySoundFX(Sound.CHECKPOINT, Channel.PLAYER_SOUND_CHECKPOINT);
            animator.SetBool("IsActive", true);
            collision.gameObject.GetComponent<Player>().CheckPointPostion = transform;
        }
    }
}
