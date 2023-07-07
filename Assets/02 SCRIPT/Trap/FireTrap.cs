using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

public class FireTrap : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float activationDelay;
    [SerializeField] float activeTime;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] AudioClip fireSound;
    [SerializeField] bool active; //when the trap is active and can hurt the player
    [SerializeField] Transform player;
    private void Awake()
    {
        StartCoroutine(ActivateFiretrap());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            if (active)
            {
                collision.GetComponentInChildren<PlayerHealth>().getDMG(damage);
            }
                   
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        while (true)
        {
            //turn the sprite red to notify the player and trigger the trap
            spriteRend.color = Color.red;

            //Wait for delay, activate trap, turn on animation, return color back to normal
            yield return new WaitForSeconds(activationDelay);
            spriteRend.color = Color.white; //turn the sprite back to its initial color
            active = true;
            anim.SetBool("active", true);
            SoundManager.Instant.PlaySound(fireSound);

            //Wait until X seconds, deactivate trap and reset all variables and animator
            yield return new WaitForSeconds(activeTime);
            active = false;
            anim.SetBool("active", false);
        }
        
    }
}