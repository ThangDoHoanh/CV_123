using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float DMG;
    [SerializeField] AudioClip spikeSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponentInChildren<PlayerHealth>().getDMG(DMG);
            SoundManager.Instant.PlaySound(spikeSound);
        }

    }
}
