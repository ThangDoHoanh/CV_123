using static PlayerController;
using UnityEngine;
using static MeleeEnemyController;
using static RangedEnemyController;
using System;

public class AnimationController : MonoBehaviour
{
    public Action OnEndAttack;

    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    //player
    public void UpdateAnimationPlayer(PlayerState playerState)
    {
        for (int i = 0; i <= (int)PlayerState.JUMP; i++)
        {
            string stateName = ((PlayerState)i).ToString();
            if (playerState == (PlayerState)i)
                _animator.SetBool(stateName, true);
            else
                _animator.SetBool(stateName, false);
        }
    }

    public void UpdateShootAnimPlayer(ShootState shootState)
    {
        _animator.SetFloat("SHOOT", (int)shootState);
    }


    //melee enemy
    public void UpdateAnimationEnemy(MeleeEnemyState enemyState)
    {
        for (int i = 0; i <= (int)MeleeEnemyState.DIE; i++)
        {
            string stateName = ((MeleeEnemyState)i).ToString();
            if (enemyState == (MeleeEnemyState)i)
                _animator.SetBool(stateName, true);
            else
                _animator.SetBool(stateName, false);
        }
    }

    //ranged enemy
    public void UpdateAnimationRangedEnemy(RangedEnemyState enemyState)
    {
        for (int i = 0; i <= (int)RangedEnemyState.DIE; i++)
        {
            string stateName = ((RangedEnemyState)i).ToString();
            if (enemyState == (RangedEnemyState)i)
                _animator.SetBool(stateName, true);
            else
                _animator.SetBool(stateName, false);
        }
    }
    public void UpdateShootAnimRangedEnemy(EnemyShootState shootState)
    {
        _animator.SetFloat("SHOOT", (int)shootState);
    }

    //event
    public void OnEndAttackAnim()
    {
        OnEndAttack?.Invoke();
    }

}