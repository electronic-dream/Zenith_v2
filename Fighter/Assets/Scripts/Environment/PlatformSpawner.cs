using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;

    public Transform platform1Pos;
    public Transform platform2Pos;

    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(platform, platform1Pos.position, platform1Pos.rotation);

            Instantiate(platform, platform2Pos.position, platform2Pos.rotation);

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }


}
