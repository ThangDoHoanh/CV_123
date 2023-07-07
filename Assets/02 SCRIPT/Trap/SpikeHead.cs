using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    
    [SerializeField]  float speed, DMG;
    [SerializeField]  float range;
    [SerializeField]  float checkDelay;
    [SerializeField]  LayerMask playerLayer;
    [SerializeField] AudioClip ImpactPlayerSound;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    [SerializeField] float checkTimer;
    [SerializeField] bool attacking;
   [SerializeField] Vector2 oldPos;
    Rigidbody2D _rigi;
    private void Awake()
    {
        oldPos = this.transform.position;
        _rigi=this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        //Move spikehead to destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
        float distanceToOldPos = Vector2.Distance(this.transform.position, oldPos); 
        if (distanceToOldPos < 0.1f) 
        {
            transform.position = oldPos;
            _rigi.velocity = Vector2.zero;
            
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();

        //Check if spikehead sees player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //Right direction
        directions[1] = -transform.right * range; //Left direction
        directions[2] = transform.up * range; //Up direction
        directions[3] = -transform.up * range; //Down direction
    }
    private void Stop()
    {
        destination = this.transform.position; //Set destination as current position so it doesn't move
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "player")
        {
            
            collision.gameObject.GetComponentInChildren<PlayerHealth>().getDMG(DMG);
            SoundManager.Instant.PlaySound(ImpactPlayerSound);
        }
        else if (collision.gameObject.tag == "door")
        {
            attacking = true;
            destination = oldPos;
            return;
            
        }
            Stop(); // D?ng trap l?i n?u va ch?m v?i b?t k? th? gì khác
        
    }

    


}
