using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemyController : MonoBehaviour
{
    Collider2D _colli;
    [SerializeField] float range, speed, movementDistance;
    
    [SerializeField] bool playerInsight, isATK;

    [SerializeField] public float DMG;

    [SerializeField] EnemyHP hp;
    [SerializeField] AudioClip attackSound;
    //References
    [SerializeField] AnimationController anim;
    
    [SerializeField] MeleeEnemyState enemyState = MeleeEnemyState.WALK;

    float _leftEdge, _rightEdge;
    bool _movingLeft;
    
    public PlayerHealth playerHealth;
    private void Awake()
    {
        _colli=this.GetComponent<Collider2D>();
        _colli.enabled = true;
        _leftEdge = this.transform.position.x - movementDistance;
        _rightEdge = this.transform.position.x + movementDistance;
        
    }

    private void Start()
    {
        Debug.DrawRay(this.transform.position,
            new Vector2(this.transform.localScale.x * range, 0), Color.red, 2);

    }

    private void Update()
    { 
        checkPlayerInSight();

        if (!playerInsight && !hp.ishurt && !hp.isdead)
        {
            Move();
        }
        UpdateState();

        anim.UpdateAnimationEnemy(enemyState);

    }
    private void Move()
    {
        Vector2 pos = this.transform.position;
        if (_movingLeft )
        {
            if (this.transform.position.x > _leftEdge)
            {
                pos = new Vector2(pos.x - speed * Time.deltaTime, pos.y);
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.transform.position = pos;
            }
            else
                _movingLeft = false;
        }
        else 
        {
            if (this.transform.position.x < _rightEdge)
            {
                pos = new Vector2(pos.x + speed * Time.deltaTime, pos.y);
                this.transform.localScale = new Vector3(1, 1, 1);
                this.transform.position = pos;
            }
            else
                _movingLeft = true;
        }
    }
    void UpdateState()
    {
        if (hp.isdead)
        {
            _colli.enabled = false;
            enemyState = MeleeEnemyState.DIE;
            StartCoroutine(DeactiveAfterTime());
        }
        else if (hp.ishurt)
        { 
            enemyState = MeleeEnemyState.HURT;
        }
        else if (playerInsight)
        {
            
            enemyState = MeleeEnemyState.ATK;
        }
        else
        {
            enemyState = MeleeEnemyState.WALK;
        }
    }

    public enum MeleeEnemyState
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
            new Vector2(this.transform.localScale.x * range, 0), 1);
        if (hit.collider == null)
        {
            playerInsight = false;
            playerHealth = null;
        }
        else
        {
            if (hit.collider.gameObject.CompareTag("player"))
            {
                playerInsight = true;
                playerHealth = hit.collider.gameObject.GetComponentInChildren<PlayerHealth>();
                anim.OnEndAttack = () =>
                {
                    playerHealth.getDMG(DMG);
                    SoundManager.Instant.PlaySound(attackSound);
                };
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
}
