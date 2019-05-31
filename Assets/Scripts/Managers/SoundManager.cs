using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : AbstractManager
{

    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        mixer.SetFloat("MasterAttenuation", SoundOn ? 0 : -80);
    }

    static string KEY_SOUND_ENABLED = "SoundEnabled";


    public bool SoundOn
    {
        get
        {
            if (!PlayerPrefs.HasKey(KEY_SOUND_ENABLED)) return true;
            return PlayerPrefs.GetInt(KEY_SOUND_ENABLED) == 1;
        }
    }


    public bool ToggleSound()
    {
        int soundStatus = 1;
        if (PlayerPrefs.HasKey(KEY_SOUND_ENABLED)) soundStatus = PlayerPrefs.GetInt(KEY_SOUND_ENABLED);
        if (soundStatus == 1) soundStatus = 0;
        else soundStatus = 1;
        PlayerPrefs.SetInt(KEY_SOUND_ENABLED, soundStatus);
        mixer.SetFloat("MasterAttenuation", SoundOn ? 0 : -80);
        return SoundOn;
    }




}
