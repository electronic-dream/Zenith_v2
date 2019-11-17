using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    // private Rigidbody2D rb;

    public float speed;
    public float startTimeToFollow;

    private float timeToFollow;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.fixedDeltaTime);

        //if (timeToFollow <= 0)
        //{

        //    timeToFollow = startTimeToFollow;
        //}
        //else
        //{
        //    timeToFollow -= Time.deltaTime;
        //}
    }
}
