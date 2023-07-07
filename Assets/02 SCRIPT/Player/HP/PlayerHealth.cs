using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float startHealth;
    [SerializeField] float inviolabletime;// tg mien nhiem
    [SerializeField] int numberflashed;// so lan nhap nhay
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float hurtime;
    [SerializeField] AudioClip hurtSound, deadSound;
    [SerializeField] AudioSource BGMusic;
    public bool ishurt, isdead; // state hurt + dead

    public float currentHealth; 

    void Awake()
    {
        BGMusic = BgMusic.Instance.Music;
        currentHealth = startHealth;
        Physics2D.IgnoreLayerCollision(12, 14, false);
        Physics2D.IgnoreLayerCollision(13, 14, false);
    }

    public void getDMG(float damage)
    {
        SoundManager.Instant.PlaySound(hurtSound);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startHealth);
        
        ishurt = true;
        StartCoroutine(Inviolabletime());
        
        if (currentHealth == 0)
        {
            isdead = true;
            SoundManager.Instant.PlaySound(deadSound);
        }
    }
    public void addHP(float hp)
    {
        currentHealth = Mathf.Clamp(currentHealth + hp, 0, startHealth);
    }
    IEnumerator Inviolabletime()
    {
        Physics2D.IgnoreLayerCollision(12,14,true);
        Physics2D.IgnoreLayerCollision(13, 14, true);
        for (int i=0;i< numberflashed; i++)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(inviolabletime/(numberflashed *2));// moi lan nhay chuyen mau 2 lan
            sprite.color = Color.white;
            yield return new WaitForSeconds(inviolabletime / (numberflashed*2) );
            
        }
        Physics2D.IgnoreLayerCollision(12, 14, false);
        Physics2D.IgnoreLayerCollision(13, 14, false);
    }
    
    public bool IsFullHealth()
    {
        return currentHealth == startHealth;
    }
}
