using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet_Trap : MonoBehaviour
{
    [SerializeField] float DMG;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;

    Rigidbody2D _rigi;

    // Start is called before the first frame update
    void Start()
    {
        _rigi = this.GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        StartCoroutine(AutoDestruct());
    }

    // Update is called once per frame
    void Update()
    {

        _rigi.velocity = speed * this.transform.right;
    }

    IEnumerator AutoDestruct()
    {
        yield return new WaitForSeconds(lifeTime);
        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "star")
        {
            this.gameObject.SetActive(false);
        }
        

        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponentInChildren<PlayerHealth>().getDMG(DMG);
        }


    }
}
