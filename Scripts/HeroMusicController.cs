using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class HeroMusicController : MonoBehaviour
{
    private AudioSource source;
    public AudioClip walkClip;
    public AudioClip SlashClip;
    public AudioClip groundClip;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Slash()
    {
        source.clip = SlashClip;
        source.loop = false;
        source.Play();
    }

    void Walk()
    {
        source.clip = walkClip;
        source.loop = false;
        source.Play();
    }

    void GroundEx()
    {
        source.clip = groundClip;
        source.loop = false;
        source.Play();
    }

}
