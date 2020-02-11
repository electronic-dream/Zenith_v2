using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    public GameObject destroyEffect;
    public Rigidbody2D bulletRb;
    public float speed;

    public bool isShootingRight = false;

    private void Start()
    {
        if (isShootingRight)
            bulletRb.velocity = transform.right * speed;
        else
            bulletRb.velocity = -transform.right * speed;
    }

    public void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health hp = collision.GetComponent<Health>();

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Boom!");

            if (!hp.immortal)
                DestroyBullet();
        }
    }
}
