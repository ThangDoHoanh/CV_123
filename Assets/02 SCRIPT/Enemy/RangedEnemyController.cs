using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MeleeEnemyController;


public class RangedEnemyController : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] Transform player;
    [SerializeField] EGunController gun;
    [SerializeField] bool playerInsight;
    
    [SerializeField] EnemyHP hp;
    //References
    [SerializeField] AnimationController anim;
    [SerializeField] RangedEnemyState RangedenemyState = RangedEnemyState.IDLE;
    [SerializeField] EnemyShootState shootState = EnemyShootState.NOTSHOOT;
    [SerializeField] AudioClip FireSound;

    Collider2D _colli;
    private void Start()
    {
        _colli=this.GetComponent<Collider2D>();
        _colli.enabled = true;
        //Debug.DrawRay(this.transform.position,
        //    new Vector2(this.transform.localScale.x * -range, 0), Color.red, range);
    }

    private void Update()
    {
        if (!hp.ishurt)
        {
            checkPlayerInSight();
        }
        CheckPlayer();
        UpdateState();
            Shoot();
        anim.UpdateAnimationRangedEnemy(RangedenemyState);
        anim.UpdateShootAnimRangedEnemy(shootState);
    }
    void CheckPlayer()
    {
        if(this.transform.position.x > player.position.x)
        {
            this.transform.localScale =new Vector3(1,1,1);
        }
        else if (this.transform.position.x < player.position.x)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void UpdateState()
    {
        if (hp.isdead)
        {
            _colli.enabled = false;
            RangedenemyState = RangedEnemyState.DIE;
            StartCoroutine(DeactiveAfterTime());
            return;
        }
         if (hp.ishurt)
        {
            RangedenemyState = RangedEnemyState.HURT;
            shootState = EnemyShootState.NOTSHOOT;
            playerInsight = false;
        }
         else
        {
            RangedenemyState = RangedEnemyState.IDLE;
        }
    }
    void Shoot()
    {
        if (hp.ishurt || hp.isdead)
            return;
        if (playerInsight)
        {
            shootState = EnemyShootState.SHOOT;
            gun.EnemyFire();
        }
        else
        {
            shootState = EnemyShootState.NOTSHOOT;
        }
    }

    public enum RangedEnemyState
    {
        IDLE,
        HURT,
        ATK,
        WALK,
        DIE
    }
    private void checkPlayerInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,
            new Vector2(this.transform.localScale.x * (-range), 0), range);
        if (hit.collider == null)
        {
            playerInsight = false;
        }
        else
        {
            if (hit.collider.gameObject.CompareTag("player"))
            {
                playerInsight = true;
            }
            else
            {
                playerInsight = false;
            }
        }
    }
    IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(0.9f);
        this.gameObject.SetActive(false);
    }

    public enum EnemyShootState
    {
        NOTSHOOT = 0,
        SHOOT = 1,
    }
    
}
