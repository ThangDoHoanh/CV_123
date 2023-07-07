using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BgMusic : MonoBehaviour
{
    private static BgMusic _instance;
    public static BgMusic Instance => _instance;
    public AudioSource Music;
    private void Awake()
    {
        this.gameObject.SetActive(true);
        if (_instance == null)
        {
            _instance = this;
            Music = this.GetComponent<AudioSource>();
            Music.Play();
            DontDestroyOnLoad(this);
            return;
        }
        if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }
}
