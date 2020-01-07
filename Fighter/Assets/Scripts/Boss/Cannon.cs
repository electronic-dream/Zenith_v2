using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public int health = 1000;
    //public GameObject deathEffectPrefab;
    public GameObject bulletPrefab;

    private float timeBtwShots;
    public float startTimeBtwShots;

    // Update is called once per frame
    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);

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
