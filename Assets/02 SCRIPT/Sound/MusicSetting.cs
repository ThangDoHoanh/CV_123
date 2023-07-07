using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour

{
    [SerializeField] Text text;
    [SerializeField] Slider musicSlider;
    AudioSource musicSource;
    // Start is called before the first frame update
    
    void Start()
    {
        
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0);
            
        }
        else
        {
            Load();
        }
        musicSource = BgMusic.Instance.Music;
    }

    // Update is called once per frame
    void Update()
    {
        
        ChangeVolume();
        text.text = "MUSIC: " + (musicSlider.value ).ToString()+"\n";
    }
    private void ChangeVolume()
    {
        musicSource.volume = musicSlider.value/100;
        Save();
    }
    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
