using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private
    Rigidbody2D _rigi;
    Collider2D _colli;

    bool _wallJumping;   
    bool _isWallSliding;

    public bool isWIN=false;

     
    [SerializeField] bool isOnGround = false, isOnWall = false;  // check ground and wall

    [SerializeField]float wallSlidingSpeed = 2;
    [SerializeField] float speed, jumpForce;//move

    [SerializeField] float jumpboost;

    [SerializeField] float rayDistance;
    
    [SerializeField] float hurtime;

    [SerializeField] PlayerHealth hp;

    [SerializeField] PGunController gun;     // guncontroller
    
    [SerializeField] PlayerState playerState = PlayerState.IDLE ;    // anim controller
    [SerializeField] ShootState shootState = ShootState.NOTSHOOT;
    [SerializeField] AnimationController anim;

    [SerializeField] AudioClip jumpSound;
    
    private void Awake()
    {
        hp.isdead = false;
        _rigi = this.GetComponent<Rigidbody2D>();
        _colli = this.GetComponent<Collider2D>();
        
    }
    // Update is called once per frame
    void Update()
    {

        CheckWallGround();
            Move();

        UpdateState(); //animation
        anim.UpdateAnimationPlayer(playerState);

        anim.UpdateShootAnimPlayer(shootState);


        Shoot();//gunATK
    }
    void Move()
    {
        if (isWIN || hp.ishurt || hp.isdead)
        {
            _rigi.velocity = Vector2.zero;
            return;
        }
        _rigi.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, _rigi.velocity.y);
        if (_rigi.velocity.x > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_rigi.velocity.x < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

        //wall sliding
        if (isOnWall)
        {
            _rigi.velocity = new Vector2(_rigi.velocity.x, Mathf.Clamp(_rigi.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        //Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
    }

    
    void Jump()
    {
        SoundManager.Instant.PlaySound(jumpSound);
        if (isOnGround)
        {
            _rigi.AddForce(new Vector2(0, jumpForce));
        }
    }
    void UpdateState()
    {
        if (isWIN)
        {
            playerState = PlayerState.WIN;
            return;
        }
        if (hp.isdead)
        {
            playerState = PlayerState.DIE;
            return;
        }
        
        if (!isOnGround)
        {
            playerState = PlayerState.JUMP;
        }
        else if (isOnGround && !isOnWall )
        {
            if (_rigi.velocity.x != 0)
            {
                playerState = PlayerState.WALK;
            }
            else
            {
                playerState = PlayerState.IDLE;
            }
        }

        if (hp.ishurt)
        {
            
            playerState = PlayerState.HURT;
            StartCoroutine(waitTime());
        }
    }
    
    void Shoot()
    {
        
        if (hp.ishurt || hp.isdead)
            return;
        if ((Input.GetKey(KeyCode.J)))
        {
            if (_rigi.velocity == Vector2.zero && isOnGround )
            {
                shootState = ShootState.SHOOT;

                gun.PlayerFire();

                return;
            }
        }
       
        shootState = ShootState.NOTSHOOT;
    }

    void CheckWallGround()
    {
        
        RaycastHit2D hit= Physics2D.Raycast(this.transform.position, Vector2.down, rayDistance);
       
        RaycastHit2D hit1 = Physics2D.Raycast(this.transform.position, new Vector2(this.transform.localScale.x, 0), 1);


        if (hit.collider == null)
        {
            isOnGround = false;

            if (hit1.collider == null)
            {
                isOnWall = false;
                _rigi.gravityScale = 3;
            }
            else
            {
                if (hit1.collider.gameObject.tag == "wall")
                {
                    isOnWall = true;
                    _rigi.gravityScale = 1;
                }
            }

        }
        else if (hit.collider.gameObject.tag == "ground" || hit.collider.gameObject.tag == "trap"
        || hit.collider.gameObject.tag == "box")
        {
            isOnGround = true;
            isOnWall = false;
            _rigi.gravityScale = 3;
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision == null)
            return;
        if (collision.gameObject.tag == "flag")
        {
            isWIN = true;
        }
        if(collision.gameObject.tag == "jumpboost")
        {
            jumpForce += jumpboost;
        }
    }

    public enum PlayerState
    {
        WIN,
        IDLE,
        WALK,
        HURT,
        DIE,
        JUMP
    }
    public enum ShootState
    {
        NOTSHOOT = 0,
        SHOOT = 1,
    }
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(hurtime);
        hp.ishurt = false;
    }

}
