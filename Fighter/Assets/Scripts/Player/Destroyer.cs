using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float timeToDestroy;

    private void Start()
    {
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject, timeToDestroy);
    }
}
