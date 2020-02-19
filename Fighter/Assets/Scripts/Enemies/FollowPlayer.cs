using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb2d;

    public float speed = 10f;
    public float attackSpeed;

    private void FixedUpdate()
    {
        if (player.position.x >= transform.position.x)
        {
            rb2d.velocity = -transform.right * attackSpeed;
        }
        else
        {
            Vector2 target = new Vector2(player.position.x, transform.position.y);

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
        }
    }
}
