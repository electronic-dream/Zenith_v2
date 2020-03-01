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
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement pM = collision.GetComponent<PlayerMovement>();
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Boom!");

            if (!pM.isDashing)
                DestroyBullet();
        }
    }
}
