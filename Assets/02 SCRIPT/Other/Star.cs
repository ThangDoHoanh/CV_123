using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] StarBarController starcontroller;
    Rigidbody2D _rigi;
    [SerializeField] float speed, lifetime;
    [SerializeField] AudioClip starSound;

    Collider2D _colli;
    private void Awake()
    {
        _rigi = this.GetComponent<Rigidbody2D>();
        _colli=this.GetComponent<Collider2D>();
        _colli.enabled = true;
    }
   
    IEnumerator DeactiveAftertime()
    {
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if(collision.gameObject.tag == "player")
        {
            _rigi.velocity = Vector2.up * speed;
            SoundManager.Instant.PlaySound(starSound);
            StartCoroutine(DeactiveAftertime());
            starcontroller.AddStar(1);
            _colli.enabled = false;
        }
    }
}
