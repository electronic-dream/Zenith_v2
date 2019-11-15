using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed = 200f;
    
    private void Start()
    {
        rb2d.velocity = transform.right * speed;           
    }
}