using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instant;
    public static SoundManager Instant => _instant;
    public AudioSource soundsource;
    
    private void Awake()
    {  
        soundsource = this.GetComponent<AudioSource>();

        if (_instant == null)
        {
            _instant = this;
            soundsource = this.GetComponent<AudioSource>();
            DontDestroyOnLoad(this);
            return;
        }
        if (_instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }
    public void PlaySound(AudioClip sound)
    {
        soundsource.PlayOneShot(sound);//phat am thanh 1 lan
    }
    
    
}
