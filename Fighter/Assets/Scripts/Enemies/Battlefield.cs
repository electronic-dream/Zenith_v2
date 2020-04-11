using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public GameObject boss;
    public Animator bossAnimator;
    public Transform[] points;

    private float timeBtwSpawn;
    public float startTimeBtwSpawn;

    float clamppedAlphaValue;

    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            //Effect showing that the boss is going to appear soon

            //The boss appearing
            int randPos = Random.Range(0, points.Length);

            boss.transform.position = points[randPos].position;

            bossAnimator.SetBool("Fading", true);

            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;

            bossAnimator.SetBool("Fading", false);
        }
    }
}
