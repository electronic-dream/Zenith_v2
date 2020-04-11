using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public float speed = 10f;

    Transform player;
    Vector3 target;

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        target = new Vector3(player.position.x, player.position.y);
    }

    private void FixedUpdate()
    {
        if (transform.position.x <= target.x && transform.position.y <= target.y)
        {
            rb2d.velocity = -transform.right * speed;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
    }
}
