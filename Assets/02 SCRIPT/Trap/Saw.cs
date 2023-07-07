using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw: MonoBehaviour
{
    [SerializeField] float DMG;
    [SerializeField] float speed;
    [SerializeField] float movementDistance;
    [SerializeField] AudioClip SawSound;
    float leftEdge, rightEdge;
    bool movingLeft;
    private void Awake()
    {
        leftEdge = this.transform.position.x - movementDistance;
        rightEdge = this.transform.position.x + movementDistance;
    }
    private void Update()

    {
        Move();
        
    }
    private void Move()
    {
        Vector2 pos = this.transform.position;
        if (movingLeft)
        {
            if (this.transform.position.x > leftEdge)
            {
                pos = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
                this.transform.position = pos;
            }
            else
                movingLeft = false;
        }
        else
        {
            if (this.transform.position.x < rightEdge)
            {
                pos = new Vector2(pos.x + speed * Time.deltaTime, pos.y);
                this.transform.position = pos;
            }
            else
                movingLeft = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            collision.gameObject.GetComponentInChildren<PlayerHealth>().getDMG(DMG);
            SoundManager.Instant.PlaySound(SawSound);
        }

    }
}
