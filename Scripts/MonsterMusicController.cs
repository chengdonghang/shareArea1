using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MonsterMusicController : MonoBehaviour
{
    private AudioSource source;
    public AudioClip attackClip;
    public AudioClip deadClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAttack()
    {
        source.clip = attackClip;
        source.loop = false;
        source.Play();
    }

    public void PlayDead()
    {
        source.clip = deadClip;
        source.loop = false;
        source.Play();
    }

}
