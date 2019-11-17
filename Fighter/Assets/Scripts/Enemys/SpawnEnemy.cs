using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject boss;
    bool spawn = true;

    void Start()
    {
        OnEnemySpawn();
    }

    void OnEnemySpawn()
    {
        if (spawn)
        {
            Instantiate(boss, transform.position, transform.rotation);
            spawn = false;
        }
    }
}
