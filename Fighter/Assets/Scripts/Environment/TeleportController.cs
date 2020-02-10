using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform player;

    [Space(1)]
    public Transform[] holes;

    public bool isAllowedToContinue = false;

    private bool goingUp = false;
    private bool goingDown = false;
    private bool goingNowhere = false;

    private void Update()
    {
        player = GameObject.Find("Player").transform;

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            goingNowhere = true;
            goingDown = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            goingDown = true;
            goingNowhere = false;
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            goingNowhere = true;
            goingUp = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            goingUp = true;
            goingNowhere = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAllowedToContinue)
            return;

        if (collision.CompareTag("Player"))
        {
            for (int i = 1; i <= holes.Length; i++)
            {
                Debug.Log(i);

                if (goingNowhere)
                    return;

                if (goingDown)
                    player.position = holes[i].position;

                if (goingUp)
                    player.position = holes[i - 1].position;

            }
        }
    }
    
    private void OnDrawGizmos()
    {
        if (holes == null || holes.Length < 1)
            return;

        for (int i = 1; i < holes.Length; i++)
        {
            Gizmos.DrawLine(holes[i - 1].position, holes[i].position);
        }
    }
}
