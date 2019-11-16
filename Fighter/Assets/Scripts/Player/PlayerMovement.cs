using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 100f;
    float horizontalMove = 0f;
    //float playerBoudaryRadius = .1f;

    [Space(10)]
    bool jump = false;
    //bool crouch = false;
    bool dash = false;

    [Space(10)]
    public Animator animator;
    public CharacterController2D controller;
    public Bounds bounds;

    [Space(10)]
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    [Space]
    private float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float setDashValue;
    private float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //animation for our player - the running one!
        //By typing "Mathf.Abs" we make the speed positive, because for the animation we can't use negative speed. It will not play the animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; 

        CameraBounds();
        Jump();
        Shoot();
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
        //Vector3 currentCameraPos = Camera.main.transform.position;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x)
            , transform.position.y
            , Mathf.Clamp(transform.position.z, -10f, -10f));
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
        dashSpeed = setDashValue;

        //checking the direction if it is 0 it means we aren't dashing
        if (direction == 0)
        {
            dash = false;
            animator.SetBool("IsDashing", false);
            //If the LShift and LeftArrow are pressed the direction sets to 1 and we spawn some particles
            if (Input.GetKey(GameManager.GM.dash) && Input.GetKeyDown(GameManager.GM.left))
            {
                direction = 1;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }
            //Else if the LShift and RightArrow are pressed the direction sets to 2 and we spawn some particles
            else if (Input.GetKey(GameManager.GM.dash) && Input.GetKeyDown(GameManager.GM.right))
            {
                direction = 2;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }
            //If we are pressing only LShift and we are facing right, then the direction sets to 3 and start spawning particles
            if (controller.m_FacingRight == true && Input.GetKeyDown(GameManager.GM.dash))
            {
                direction = 3;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }

            //else if we are facing left and LShift is pressed, then the direction sets to 4 and spawns particles as well
            else if (controller.m_FacingRight == false && Input.GetKeyDown(GameManager.GM.dash))
            {
                direction = 4;
                Instantiate(particleSpawn, transform.position, Quaternion.identity);
                dash = true;
            }
            if (controller.m_FacingRight == true && dash == true && Input.GetKeyDown(GameManager.GM.left))
            {
                direction = 5;
                controller.m_FacingRight = true;
                dash = true;
            }
            else if (controller.m_FacingRight == false && dash == true && Input.GetKeyDown(GameManager.GM.right))
            {
                direction = 6;
                controller.m_FacingRight = false;
                dash = true;
            }
        }
        else
        {
            //If we ran out of juice the direction sets to 0 because we aren't dashing
            if (dashTime <= 0)
            {
                direction = 0;
                controller.m_Rigidbody2D.velocity = Vector2.zero;
                controller.m_Rigidbody2D.gravityScale = 12f;
                //resseting the spawn dash time
                dashTime = startDashTime;
            }
            //else if the time btw dash is not 0
            else
            {
                //we slowly decrease the dash time
                dashTime -= Time.deltaTime;

                //if we move to the left with LShift and LeftArrow presssed..
                if (direction == 1)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);

                }
                //else we will dash into the opposite direction
                else if (direction == 2)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);
                }
                //but if we dash with only LShift and we are facing right we will dash right without having to press arrows
                if (direction == 3)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.right * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);
                }
                //Same thing here but with the opposite direction
                else if (direction == 4)
                {
                    controller.m_Rigidbody2D.velocity = Vector2.left * dashSpeed;
                    controller.m_Rigidbody2D.gravityScale = 0f;
                    animator.SetBool("IsDashing", true);
                }
                if (direction == 5)
                {
                    animator.SetBool("IsDashing", true);
                }
                else if (direction == 6)
                {
                    animator.SetBool("IsDashing", true);
                }

                if (activNoDmg)
                {
                    if (direction == 1 || direction == 2 || direction == 3 || direction == 4 || direction == 5 || direction == 6)
                    {
                        health.noDmg = true;
                    }
                    else if (direction == 0)
                        health.noDmg = false;
                }
            }
        }
    }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    [SerializeField] private GameObject poofEffect;
    [SerializeField] private Animator particleAnimator;
    private void CanInstantiateParticles(GameObject particleSpawn)
    {
        if (!particleAnimator.GetCurrentAnimatorStateInfo(1).IsName("Poof"))
        {
            dash = true;
            isDashing = true;
            Instantiate(particleSpawn, transform.position, Quaternion.identity);
        }
        else if (poofEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Poof"))
        {
            dash = false;
            isDashing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            animator.SetBool("IsDashing", false);
        }
    }

    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

=======
>>>>>>> parent of feecaaa... Level1-AlmostDone!
=======
>>>>>>> parent of feecaaa... Level1-AlmostDone!
=======
>>>>>>> parent of feecaaa... Level1-AlmostDone!
    public void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        //crouch = false;
        jump = false;
        dash = false;
    }
}
