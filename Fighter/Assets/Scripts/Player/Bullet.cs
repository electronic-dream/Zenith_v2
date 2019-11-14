using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    
    private void Update()
    {
        rb2d.velocity = transform.right * speed * Time.deltaTime;
    }
}
