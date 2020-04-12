using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBoss : MonoBehaviour
{
    //public GameObject deathEffectPrefab;

    public int health = 1;
    public bool isInvincible = false;
    public int mosquitosCount = 5;

    public NextLevel nxtLevel;
    public GameObject question;

    public bool isLastBoss = false;

    //if we are not going to use the gameobjects below then we can set this bool to be equal to false,
    //the same goes when we are going to use them
    public bool isUsabble = true;

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            health -= damage;

            if (health <= 0)
            {
                //Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                Destroy(gameObject);

                if (isLastBoss)
                {
                    nxtLevel.isAllowedToContinue = true;
                    question.SetActive(true);
                }
            }
        }
    }

    public GameObject bubbleEffectShowing;
    public GameObject bubbleEffectGoingDown;

    private void Update()
    {
        if (isUsabble)
        {
            if (isInvincible)
            {
                bubbleEffectShowing.SetActive(true);
                bubbleEffectGoingDown.SetActive(false);
            }
            else if (!isInvincible)
            {
                bubbleEffectShowing.SetActive(false);
                bubbleEffectGoingDown.SetActive(true);
            }
        }
    }

    public GameObject enemyPrefab;

    public void SpawnEnemies()
    {
        if (isUsabble)
        {
            StartCoroutine(Spawning());
        }
    }

    IEnumerator Spawning()
    {
        int i = 0;

        float enemySpawnMinY = transform.position.y + 26f;
        float enemySpawnMaxY = transform.position.y + 6f;
        float enemySpawnMinX = transform.position.x - 11f;
        float enemySpawnMaxX = transform.position.x + 11f;

        while (i < mosquitosCount)
        {
            Vector2 pos = new Vector2(Random.Range(enemySpawnMinX, enemySpawnMaxX), Random.Range(enemySpawnMinY, enemySpawnMaxY));

            Instantiate(enemyPrefab, pos, Quaternion.identity);

            yield return new WaitForSeconds(1f);

            i++;
        }
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
