using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Animator bossAnimator;
    public Bounds bounds;

    GameObject player;
    BoxCollider2D boxCollider;

    private void Start()
    {
        player = GameObject.Find("Player");
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            bounds.locked = true;
            boxCollider.enabled = false;
        }
    }
}
