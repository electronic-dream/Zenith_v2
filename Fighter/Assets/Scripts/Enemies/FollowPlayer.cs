using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Boss boss;

    public float speed = 10f;

    private void FixedUpdate()
    {
        Vector2 target = new Vector2(player.position.x, transform.position.y);

        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);

        transform.position = this.transform.position;
    }
}
