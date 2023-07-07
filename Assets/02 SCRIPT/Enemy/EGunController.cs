using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EGunController : MonoBehaviour
{
    [SerializeField] GameObject ebulletPrefab;
    [SerializeField] float timeFireCount;
    [SerializeField] float fireSpeed;
    [SerializeField] AudioClip fireballSound;
    [SerializeField] EnemyHP hp;
    void Update()
    {
        if (!hp.ishurt)
        {
            timeFireCount -= Time.deltaTime;
        }
        
    }
    public void EnemyFire()
    {
        if (timeFireCount > 0)
        {
            return;
        }

        timeFireCount = fireSpeed;

        GameObject bullet1 = ObjectPooling.instant.GetObject(ebulletPrefab.gameObject);

        bullet1.transform.position = this.transform.position;
        bullet1.transform.rotation = Quaternion.Euler(0, this.transform.parent.localScale.x == 1 ? 0 : 180, 0);
        bullet1.gameObject.SetActive(true);
        SoundManager.Instant.PlaySound(fireballSound);
    }
}
