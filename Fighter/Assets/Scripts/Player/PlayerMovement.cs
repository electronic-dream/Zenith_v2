using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 100f;
    float horizontalMove = 0f;
    float playerBoudaryRadius = .1f;

    [Space(10)]
    bool jump = false;
    bool crouch = false;
    bool dash = false;

    [Space(10)]
    public Animator animator;
    public CharacterController2D controller;
    public Bounds bounds;

    [Space(10)]
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

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
        Vector3 currentCameraPos = Camera.main.transform.position;
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds.leftBound.position.x, maxCameraPos.x)
            , transform.position.y
            , Mathf.Clamp(transform.position.z, -10f, -10f));
    }

    public void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        crouch = false;
        jump = false;
        dash = false;
    }
}
