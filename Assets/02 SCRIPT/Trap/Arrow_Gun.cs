using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Gun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireSpeed = 3;
    [SerializeField] float timeFireCount = 0;
    [SerializeField] AudioClip trapBulletSound;
    [SerializeField] Transform player;

    void Update()
    {
        timeFireCount -= Time.deltaTime;
        if (timeFireCount > 0)
        {
            return;
        }
        Fire();
    }

    public void Fire()
    {
        timeFireCount = fireSpeed;
        SoundManager.Instant.PlaySound(trapBulletSound);
        GameObject bullet = ObjectPooling.instant.GetObject(bulletPrefab);

        bullet.transform.position = this.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0, this.transform.parent.localScale.x == 1 ? 0 : 180, 
            this.transform.parent.rotation.z == 0 ? 0:-90);
        bullet.gameObject.SetActive(true);

    }
}
