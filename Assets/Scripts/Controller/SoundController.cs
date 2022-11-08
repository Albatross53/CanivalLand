using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] Slider back_slider;
    [SerializeField] Slider effect_slider;

    void Start()
    {
        back_slider.value = SoundManager.back_Volum;
        effect_slider.value = SoundManager.effect_Volum;

        back_slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
        effect_slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectVolume(val));
    }

}
