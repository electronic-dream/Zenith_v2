using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpBehaviour : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;
    public float speed;

    private Transform playerPos;
    private Vector2 target;
    private SpriteRenderer spriteRenderer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(maxTime, minTime);
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        spriteRenderer = GameObject.FindGameObjectWithTag("Boss").GetComponent<SpriteRenderer>();

        target =
            new Vector2(playerPos.position.x, animator.transform.position.y);
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

            if (playerPos.transform.position.x > animator.transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (playerPos.transform.position.x < animator.transform.position.x)
            {
                spriteRenderer.flipX = false;
            }

            animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
