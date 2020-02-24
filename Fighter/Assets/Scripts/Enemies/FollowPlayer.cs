using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public float speed = 10f;

    private void FixedUpdate()
    {
        Transform player = GameObject.Find("Player").transform;

        if (player.position.x >= transform.position.x)
        {
            rb2d.velocity = -transform.right * speed;
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, player.position.y);

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }
}
