using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class PGunController : MonoBehaviour
{
    [SerializeField] PlayerBullet pbulletPrefab;
    [SerializeField] float fireSpeed = 1;
    [SerializeField] float timeFireCount = 1;
    [SerializeField] AudioClip fireballSound;
    void Update()
    {
        timeFireCount -= Time.deltaTime;
    }

    public void PlayerFire()
    {
        if (timeFireCount > 0)
        {
            return;
        }

        timeFireCount = fireSpeed;

        GameObject bullet = ObjectPooling.instant.GetObject(pbulletPrefab.gameObject);

        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0, this.transform.parent.localScale.x == 1 ? 0 : 180, 0);
        bullet.gameObject.SetActive(true);
        SoundManager.Instant.PlaySound(fireballSound);
    }
    
}
