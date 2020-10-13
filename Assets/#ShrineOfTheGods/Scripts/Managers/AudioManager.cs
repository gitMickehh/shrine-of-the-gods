using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource mySource;
    private AudioSource myBGMusicSource;
    public S_AudioManager myManager;

    private void Awake()
    {
        mySource = gameObject.AddComponent<AudioSource>();
        mySource.playOnAwake = false;

        myBGMusicSource = gameObject.AddComponent<AudioSource>();
        mySource.playOnAwake = false;
    }

    private void OnEnable()
    {
        myManager.Value = this;
    }

    private void OnDisable()
    {
        myManager.Value = null;
    }

    public void MusicOff()
    {
        StopCurrent(mySource);
        StopCurrent(myBGMusicSource);
    }

    public void PlaySound(S_Sound sound)
    {
        
        // Debug.Log("Playing sound: " + sound.name);

        if (!sound.backgroundMusic)
        {
            StopCurrent(mySource);
            // Debug.Log("Playing sound");

            mySource.clip = sound.clip;
            mySource.panStereo = sound.panning;
            mySource.volume = sound.volume;
            mySource.loop = sound.looping;

            mySource.PlayOneShot(sound.clip);
        }
        else
        {
            StopCurrent(myBGMusicSource);

            myBGMusicSource.clip = sound.clip;
            myBGMusicSource.panStereo = sound.panning;
            myBGMusicSource.volume = sound.volume;
            myBGMusicSource.loop = sound.looping;

            myBGMusicSource.Play();
        }

    }

    public void MusicOn()
    {
        if(!myBGMusicSource.isPlaying)
            myBGMusicSource.Play();
    }

    public void StopCurrent(AudioSource source)
    {
        if (source.isPlaying)
            source.Pause();
    }

}
