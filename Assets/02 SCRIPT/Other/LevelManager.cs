using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PlayerController player;

    [SerializeField] GameObject winScene;
    [SerializeField] GameObject gameOverScene;
    [SerializeField] GameObject PauseScene;

    [SerializeField] PlayerHealth hpPlayer;
    [SerializeField] float timeStartScene;

    [SerializeField] GameObject PauseButton;

    [SerializeField] AudioSource BGMusic;

    [SerializeField] EnemyHP enemy;

    [SerializeField] GameObject WallBlock;

    [SerializeField] AudioClip winsound;
    [SerializeField] AudioClip oversound;

    [SerializeField] bool CanplaySound;
    AudioSource SoundEffect;
    void Awake()
    {
        SoundEffect = SoundManager.Instant.soundsource;
        BGMusic =BgMusic.Instance.Music;
        Time.timeScale = 1;
        PauseScene.SetActive(false);
        winScene.SetActive(false);
        gameOverScene.SetActive(false);
        PauseButton.SetActive(true);
        WallBlock.SetActive(true);
        CanplaySound = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGame();
    }
    void CheckGame()
    {
        if (player.isWIN)
        {
            if (SoundEffect.volume > 0 && CanplaySound)
            {
                SoundManager.Instant.PlaySound(winsound);
                CanplaySound = false;
            }

            PauseButton.SetActive(false);
            
            StartCoroutine(StartWinScene());
            
            return;
        }
        if (hpPlayer.isdead)
        {
            if (SoundEffect.volume > 0 && CanplaySound)
            {
                SoundManager.Instant.PlaySound(oversound);
                CanplaySound = false;
            }
            
            PauseButton.SetActive(false);
            StartCoroutine(StartgameOverScene());
            
            return;
        }

        if (enemy.isdead)
        {
            WallBlock.SetActive(false);
        }
    }
    public void Resume()
    {
        PauseScene.SetActive(false);
        Time.timeScale = 1;
    }
    public void Pause()
    {
        PauseScene.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator StartWinScene()
    {
        yield return new WaitForSeconds(timeStartScene);
        winScene.SetActive(true);
        
        Time.timeScale = 0;
       
    }
    IEnumerator StartgameOverScene()
    {
        yield return new WaitForSeconds(timeStartScene);
        gameOverScene.SetActive(true);
        
        Time.timeScale = 0;
        
    }
}
