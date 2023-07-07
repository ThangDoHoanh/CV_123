using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPCollect : MonoBehaviour
{
    [SerializeField] float hpAdd;
    [SerializeField] AudioClip HPCollectSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            PlayerHealth _playerHealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            if(!_playerHealth.IsFullHealth() && _playerHealth.currentHealth > 0)
            {
                SoundManager.Instant.PlaySound(HPCollectSound);
                _playerHealth.addHP(hpAdd);
                this.gameObject.SetActive(false);
            }
        }

    }
}
