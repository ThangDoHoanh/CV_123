using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColor());
    }
    IEnumerator ChangeColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            sprite.color = Color.black;
        }
        
    }
}
