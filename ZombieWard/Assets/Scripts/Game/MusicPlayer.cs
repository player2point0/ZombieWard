using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip[] Tracks;

    private int index;
    private AudioSource musicPlayer;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        index = 0;
        musicPlayer = GetComponent<AudioSource>();
        PlayTrack();
    }

    private void Update()
    {
        if (!musicPlayer.isPlaying) PlayTrack();
    }

    void PlayTrack()
    {
        musicPlayer.clip = Tracks[index];
        musicPlayer.Play();

        index++;

        if (index >= Tracks.Length) index = 0;
    }
}
