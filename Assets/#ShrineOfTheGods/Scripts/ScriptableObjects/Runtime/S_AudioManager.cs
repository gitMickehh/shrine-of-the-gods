using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Audio Manager", menuName = "Basic/Audio/Audio Manager")]
public class S_AudioManager : S_RuntimeObject<AudioManager>
{
    public S_Bool audioToggle;

    public void PlaySound(S_Sound sound)
    {
        if(!audioToggle.Value)
        {
            Value.MusicOff();
            return;
        }
        else
        {
            Value.MusicOn();
        }
        
        Value.PlaySound(sound);
    }
}
