using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static float effect_Volum = 0.5f;
    public static float back_Volum = 0.5f;
    [SerializeField] AudioSource back_Source;
    [SerializeField] AudioSource effect_Source;

    static SoundManager g_soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            g_soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ChangeMasterVolume(back_Volum);
        ChangeMasterVolume(effect_Volum);
    }


    public void PlayEffectSound(AudioClip clip)
    {
        effect_Source.PlayOneShot(clip);
    }

    public void PlayBackSound(AudioClip clip)
    {
        back_Source.clip = clip;
        back_Source.Play();
    }

    public void ChangeMasterVolume(float value)
    {
        back_Source.volume = value;
        back_Volum = value;
    }

    public void ChangeEffectVolume(float value)
    {
        effect_Source.volume = value;
        effect_Volum = value;
    }


    public static SoundManager Instance
    {
        get { return g_soundManager; }
    }
}
