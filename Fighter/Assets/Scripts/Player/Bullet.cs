using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 2;
    public float speed = 20f;

    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Boss boss = hitInfo.GetComponent<Boss>();
        Cannon cannon = hitInfo.GetComponent<Cannon>();

        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }

        if(cannon != null)
        {
            cannon.TakeCanonDamage(damage);
            Destroy(gameObject);
        }

        if(hitInfo.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
