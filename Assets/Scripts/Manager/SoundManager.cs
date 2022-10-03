using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static float effect_Volum;
    public static float back_Volum;

    /*
    [Header("back")]
    [SerializeField] AudioClip title;
    [SerializeField] AudioClip dorm;
    [SerializeField] AudioClip main;
    [SerializeField] AudioClip safari;
    [SerializeField] AudioClip flumride;
    [SerializeField] AudioClip rollercoaster;
    [SerializeField] AudioClip restaurant;
    */

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
    }


    public void PlayEffectSound(AudioClip clip)
    {
        effect_Source.PlayOneShot(clip);
    }

    public void PlayBackSound(AudioClip clip)
    {
        back_Source.PlayOneShot(clip);
    }


    public static SoundManager Instance
    {
        get { return g_soundManager; }
    }
}
