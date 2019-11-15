using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform playerPos;
    public Rigidbody2D rb2d;
    public float speed;
    
    private void Start()
    {
        playerPos = GameObject.Find("Player").transform;

        if (playerPos.rotation.y == 180f)
        {
            rb2d.velocity = -transform.right * speed * Time.deltaTime;
        }
     
           
    }
}