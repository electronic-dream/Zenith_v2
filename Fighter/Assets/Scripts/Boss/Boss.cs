using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int health = 1000;
    public int takenDmg = 1;
    public int stageTwoHp;
    
    public Animator anim;
    public Animator numbersAnim;
    public GameObject beforeSpawnEffect;
    private GameObject boss;
    public Collider2D col2D;
    public GameObject beforeSpawnPrefab;
    public GameObject lightningPrefab;
    public GameObject disColl;

    [Space(3)]
    private float timeBtwSpawn = 10;
    public float theCountdown = 10;
    public float waitSecs;
    public float waitSecsStTwo;
    public float wait;

    [Header("X spawn range")]
    public float minX;
    public float maxX;

    [Header("Y spawn range")]
    public float minY;
    public float maxY;

    private void Start()
    {
        boss = GameObject.Find("Boss");
        anim = boss.GetComponent<Animator>();

        theCountdown = timeBtwSpawn;

        InvokeRepeating("BossBattle", 0f, 5f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= stageTwoHp)
        {
            anim.SetTrigger("stageTwo");
        }
        if (health <= 0)
        {
            anim.SetTrigger("death");
            //StartCoroutine(Die());
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
