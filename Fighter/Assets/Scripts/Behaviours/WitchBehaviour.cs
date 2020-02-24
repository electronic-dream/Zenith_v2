using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchBehaviour : StateMachineBehaviour
{
    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("attack");
        }
        else
        {
            timer -= Time.deltaTime;
            animator.transform.rotation = Quaternion.RotateTowards(animator.transform.rotation, playerPos.rotation, .1f * Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
