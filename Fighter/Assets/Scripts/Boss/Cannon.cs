using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int health = 1000;
    //public GameObject deathEffectPrefab;
    public GameObject bulletPrefab;
    public Transform firePoint;
 
    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        if (timeBtwShots <= 0)
        {
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
            //Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
