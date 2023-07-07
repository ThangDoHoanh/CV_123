using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] float dmgE;

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
        if (collision == null) 
                return;

        if (collision.gameObject.tag != "enemy")
        {
            this.gameObject.SetActive(false);
            if(collision.gameObject.tag == "player")
            {
                collision.gameObject.GetComponentInChildren<PlayerHealth>().getDMG(dmgE);
            }
        }
    }
}
