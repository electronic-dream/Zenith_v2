using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int health = 1000;
    //public GameObject deathEffectPrefab;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public TeleportController teleportController;

    public bool canShoot = false;
    public bool isFacingRight = false;
    public bool isLastCannon = false;

    public GameObject question;
    public NextLevel nextLevel;

    private float timeBtwShots;
    public float startTimeBtwShots;

    //private void Start()
    //{
    //    if (isLastCannon)
    //        question = GetComponent<GameObject>();
    //}

    void Update()
    {
        if (!canShoot)
            return;

        if (timeBtwShots <= 0)
        {
            bulletPrefab.GetComponent<BombBullet>().isShootingRight = isFacingRight;

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    public void TakeCanonDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            teleportController.isAllowedToContinue = true;

            if (isLastCannon)
            {
                question.SetActive(true);
                nextLevel.isAllowedToContinue = true;
            }

            Destroy(gameObject);
        }
    }
}
