using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 100f;
    float horizontalMove = 0f;

    [Space(10)]
    bool jump = false;
    bool dash = false;

    [Space(10)]
    public Animator animator;
    public GameObject particleSpawn;
    public SpriteRenderer spriteRenderer;

    [Space(10)]
    public CharacterController2D controller;
    public Bounds bounds;
    public Health health;
    public Boss boss;

    public bool isDashing = true;
    public bool isMoving = true;
    public bool restricted = false;
    public bool canBeRestricted = false;


    [Space]
    private float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float setDashValue;
    private float timer;

    //public float coordsToReach;

    public bool activNoDmg = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //animation for our player - the running one!
        //By typing "Mathf.Abs" we make the speed positive, because for the animation we can't use negative speed. It will not play the animation

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (isMoving)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }

        CameraBounds();
        Jump();
        Shoot();

        if (canBeRestricted)
            RestrictPlayer();

        if (isDashing)
        {
            Dash();
        }
    }

    void RestrictPlayer()
    {
        if (boss.health > 0)
        {
            restricted = true;
            bounds.locked = true;
        }
        else
        {
            restricted = false;
            bounds.locked = false;
        }
    }

    void Jump()
    {
        //Jumping with Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    void Shoot()
    {
        //Shooting with Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsShooting", true);
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetBool("IsShooting", false);
        }
    }

    void CameraBounds()
    {
        //We set the bounds to the player - when he moves on the x axis or the left or right he will stop at certain point because of our constraints,
        //but when he moves on the y axis or up and down we will stop at certain point because of constraints we set! 
        if (!restricted)
        {
            transform.position = new Vector3(

                Mathf.Clamp(
                    transform.position.x,
                    bounds.minCameraBounds.x - Vector3.Distance(bounds.minCameraBounds, bounds.leftBound.position)
                    , bounds.maxCameraBounds.x + Vector3.Distance(bounds.maxCameraBounds, bounds.rightBound.position))

                , transform.position.y

                , Mathf.Clamp(transform.position.z, -5f, -5f));
        }

        if (restricted)
        {
            transform.position = new Vector3(

               Mathf.Clamp(
                   transform.position.x,
                   bounds.currentCameraBounds.x - Vector3.Distance(bounds.currentCameraBounds, bounds.leftBound.position)
                   , bounds.minCameraBounds.x + Vector3.Distance(bounds.minCameraBounds, bounds.rightBound.position))

               , transform.position.y

               , Mathf.Clamp(transform.position.z, -5f, -5f));
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnDashing(bool isDashing)
    {
        animator.SetBool("IsDashing", isDashing);
    }

    public void Dash()
    {
        //checking the direction if it is 0 it means we aren't dashing
        if (direction == 0)
        {
            dashSpeed = setDashValue;

            dash = false;
            animator.SetBool("IsDashing", false);

            //If we are facing Right and pressing LShift, then the direction sets to 1 and start spawning particles
            if (!controller.m_FacingRight && Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("IsDashing", true);
                dash = true;

                direction = 1;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);

                controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
            }
            //if we are facing Right and LShift is pressed, then the direction sets to 2 and spawns particles as well
            if (controller.m_FacingRight && Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("IsDashing", true);
                dash = true;

                direction = 2;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);

                controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;

                controller.m_Rigidbody2D.velocity = Vector2.zero;

                //Instantiate(particleSpawn, transform.position, Quaternion.identity);

                //resseting the spawn dash time
                dashTime = startDashTime;
            }
            else
                dashTime -= Time.deltaTime;
        }

        if (direction == 1 || direction == 2)
        {
            health.immortalWhileDashing = true;
        }
        else if (direction == 0)
            health.immortalWhileDashing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            animator.SetBool("IsDashing", false);
        }
    }

    public void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        //crouch = false;
        jump = false;
        dash = false;
    }
}
