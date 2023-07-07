using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField] AudioClip Sound;
    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        StartCoroutine(changeColor());
    }
    // Update is called once per frame
    IEnumerator changeColor()
    {
        while (this.gameObject.activeSelf)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.tag == "player")
        {
            SoundManager.Instant.PlaySound(Sound);
            this.gameObject.SetActive(false);
        }
    }
}
