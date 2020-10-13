using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "Basic/Audio/Sound")]
public class S_Sound : ScriptableObject
{
    public AudioClip clip;
    public float panning;
    public float volume;
    public bool looping;
    [Tooltip("This is on a separate audio source")]public bool backgroundMusic;

}
