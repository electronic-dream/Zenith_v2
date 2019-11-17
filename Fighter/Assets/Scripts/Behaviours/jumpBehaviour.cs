using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpBehaviour : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(maxTime, minTime);
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }

        Vector2 target =
            new Vector2(playerPos.position.x, animator.transform.position.y);

        Vector2 lastPlayerPos = target;
        
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, lastPlayerPos, speed * Time.deltaTime);
        
        //animator.transform.Rotate(0f, playerPos.rotation.y, 0f);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
