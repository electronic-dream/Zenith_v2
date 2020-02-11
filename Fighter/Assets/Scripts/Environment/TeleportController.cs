using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform player;
    public Cannon cannon;

    [Space(1)]
    public Transform[] holes;

    public bool isAllowedToContinue = false;

    private void Update()
    {
        player = GameObject.Find("Player").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAllowedToContinue)
            return;

        if (collision.CompareTag("Player"))
        {
            cannon.canShoot = true;

            for (int i = 1; i < holes.Length; i++)
            {
                player.position = holes[i].position;
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
