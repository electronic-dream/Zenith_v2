using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public PlayerMovement pM;
    public GameObject bossGO;
    public GameObject bossParticles;

    private bool canDetect = true;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (canDetect)
        {
            if (other.CompareTag("Player"))
            {
                canDetect = false;
                StartCoroutine(WaitBeforeRestricting());
            }
        }
    }

    IEnumerator WaitBeforeRestricting()
    {
        yield return new WaitForSecondsRealtime(.5f);

        pM.canBeRestricted = true;
        EnableBoss();
    }

    void EnableBoss()
    {
        Instantiate(bossParticles, bossGO.transform.position, bossGO.transform.rotation);
        bossGO.SetActive(true);
    }
}
