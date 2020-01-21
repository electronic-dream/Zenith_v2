using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    public bool isMoving;
    public Rigidbody2D rb2d;
    public float speed;
    public float timeToDestroy;

    private void Start()
    {
        if (isMoving)
        {
            rb2d.velocity = transform.right * speed;
            DestroyPlatform(timeToDestroy);
        }
    }

    public void DestroyPlatform()
    {
        Destroy(this.gameObject);
    }

    public void DestroyPlatform(float time)
    {
        Destroy(this.gameObject, time);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        Debug.Log("Boom!");

    //        DestroyPlatform(2);
    //    }
    //}
}
