using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBoss : MonoBehaviour
{
    //public GameObject deathEffectPrefab;

    public int health = 1;
    public bool isInvincible = false;

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;

            if (health <= 0)
            {
                //Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public GameObject enemyPrefab;
    public float enemySpawnMinY;
    public float enemySpawnMaxY;

    public void SpawnEnemies()
    {
        Vector2 pos = new Vector2(transform.position.x, Random.Range(enemySpawnMinY, enemySpawnMaxY));

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    public void SetInvincibleTrue()
    {
        isInvincible = true;
    }

    public void SetInvincibleFalse()
    {
        isInvincible = false;
    }
}
