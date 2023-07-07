using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [SerializeField] float lifetime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;
        if (collision.gameObject.tag == "player")
        {
            StartCoroutine(DeactiveAftertime());
        }
    }
    IEnumerator DeactiveAftertime()
    {
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    } 
}
