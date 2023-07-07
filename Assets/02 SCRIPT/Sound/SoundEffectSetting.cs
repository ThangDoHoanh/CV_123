using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectSetting : MonoBehaviour

{
    [SerializeField] Text text;
    [SerializeField] Slider SoundESlider;
    [SerializeField] AudioSource SoundEffect;
    // Start is called before the first frame update

    void Start()
    {

        if (!PlayerPrefs.HasKey("SoundEffect"))
        {
            PlayerPrefs.SetFloat("SoundEffect", 0);

        }
        else
        {
            Load();
        }
        SoundEffect = SoundManager.Instant.soundsource;
    }

    // Update is called once per frame
    void Update()
    {

        ChangeVolume();
        text.text = "SOUND EFFECT: " + (SoundESlider.value).ToString() + "\n";
    }
    private void ChangeVolume()
    {
        SoundEffect.volume = SoundESlider.value / 100;
        Save();
    }
    private void Load()
    {
        SoundESlider.value = PlayerPrefs.GetFloat("SoundEffect");
    }
    void Save()
    {
        PlayerPrefs.SetFloat("SoundEffect", SoundESlider.value);
    }
}
