using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int health = 1000;

    private GameObject boss;
    public GameObject deathEffectPrefab;
    public GameObject question;

    public NextLevel nxtLevel;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);

            nxtLevel.isAllowedToContinue = true;
            question.SetActive(true);
        }
    }

    //IEnumerator Die()
    //{
    //    if (theCountdown <= 0)
    //    {
    //        theCountdown = timeBtwSpawn;
    //    }
    //    else
    //    {
    //        while (health <= 0)
    //        {
    //            Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

    //            GameObject explEff = explosionEff[Random.Range(0, explosionEff.Length)];

    //            yield return new WaitForSeconds(1.6f);
    //            Instantiate(explEff, pos, Quaternion.identity);

    //            Destroy(gameObject, 3f);
    //            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //        }
    //        theCountdown -= Time.deltaTime;
    //    }
    //}
}
