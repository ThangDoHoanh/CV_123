using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float getDmg, timechangestate; //getDMG <= 1
    [SerializeField] AudioClip hurtSound, deadSound;
    float _startHP;

    // _startHealth= 0.3
    public bool ishurt, isdead; // state hurt + dead
    
    public float currentHP;
    
    
    void Awake()
    {
        _startHP = this.transform.localScale.x;
        currentHP = _startHP;
    }
    
    // Update is called once per frame
    public void getDMG()
    {
        if (currentHP > 0)
        {
            SoundManager.Instant.PlaySound(hurtSound);
            currentHP -= _startHP * getDmg;
            this.transform.localScale = new Vector3(currentHP, this.transform.localScale.y,
                this.transform.localScale.z);
            ishurt = true;
            StartCoroutine(changeState());
        }  
        if (currentHP == 0)
        {
            SoundManager.Instant.PlaySound(deadSound);
            isdead = true;
            ishurt = false;
            this.gameObject.SetActive(false);
            return;
        }    
    }
    IEnumerator changeState()
    {
        yield return new WaitForSeconds(timechangestate);
        ishurt = false;
    }
}
