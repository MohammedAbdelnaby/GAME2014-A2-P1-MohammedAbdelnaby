using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public List<AudioSource> channels;
    public List<AudioClip> audioClips;

    // Start is called before the first frame update
    void Awake()
    {
        channels = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitializeSoundFX();
    }

    private void InitializeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/Jump"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/FruitPickedUp"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/EnemyJumpedOn"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/CheckPointReached"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/EndReached"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Death"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Level_1"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Level_2"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/MainMenu_n_Win"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/Lost"));
    }
    public void PlaySoundFX(Sound sound, Channel channel)
    {
        channels[(int)channel].clip = audioClips[(int)sound];
        if (channel == Channel.MUSIC)
        {
            channels[(int)channel].loop = true;
            channels[(int)channel].volume = 0.2f;
        }
        channels[(int)channel].Play();
    }
}
