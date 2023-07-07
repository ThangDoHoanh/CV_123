using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] AudioClip changeSound;
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] Slider LoadingBarSlider;
    [SerializeField] float MaxValueSilder=1;
    float startTime;
    private void Awake()
    {
        LoadingScreen.SetActive(false);
        LoadingBarSlider.maxValue = MaxValueSilder;
        LoadingBarSlider.minValue = 0;
        LoadingBarSlider.value = 0;
    }

    public void Load_Scene(int index)
    {
        SoundManager.Instant.PlaySound(changeSound);
        SceneManager.LoadScene(index);
    }
    public void Load_ScenehasLoading(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
        SoundManager.Instant.PlaySound(changeSound);
    }
    public void QuitGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        
    }

    

    IEnumerator LoadSceneAsync(int index)
    {
        
        startTime = Time.realtimeSinceStartup;
        LoadingScreen.SetActive(true);

        float currentProgress = 0f;

        while (currentProgress < LoadingBarSlider.maxValue)
        {
            
            float deltaTime = Time.realtimeSinceStartup - startTime;
            
            currentProgress += deltaTime/500f; 
            LoadingBarSlider.value = currentProgress;
            yield return null;
        }

        SceneManager.LoadSceneAsync(index);
        yield return new  WaitForSeconds(1);
        LoadingScreen.SetActive(false);
    }

}
