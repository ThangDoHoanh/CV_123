using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameObject Prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="bullet")
        {
            this.gameObject.SetActive(false);
            GameObject g = ObjectPooling.instant.GetObject(Prefab);
            g.transform.position = this.transform.position;
            g.SetActive(true);
        }
    }
}
