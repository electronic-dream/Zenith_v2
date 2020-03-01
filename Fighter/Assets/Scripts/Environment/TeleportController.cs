using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TeleportController : MonoBehaviour
{
    public Transform player;
    public Cannon cannon;

    [Space(1)]
    public Transform[] holes;

    public bool isAllowedToContinue = false;
    bool teleported = false;

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
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        for (int i = 1; i < holes.Length; i++)
        {
            yield return new WaitForSeconds(.5f);
            player.position = holes[i].position;
        }

        player.GetComponent<Health>().immortal = true;
        Cannon.canShoot = true;

        yield return new WaitForSeconds(1f);

        player.GetComponent<Health>().immortal = false;
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
