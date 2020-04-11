using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public enum Level
    {
        First,
        Second,
        Third
    }

    public Level currentLevel = Level.First;

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
            //bounds.locked = true;
            
            boxCollider.enabled = false;
        }
    }
}
