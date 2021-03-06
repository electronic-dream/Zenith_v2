﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public static int health = 1;
    [HideInInspector] public static bool askedQuestion = false;

    public int numOfHearts;
    [SerializeField]
    public bool immortal = false;
    public bool immortalWhileDashing = false;

    //public float speed;
    //public float minX;
    //public float maxX;
    //public float playerPos;
    //public float m_Speed;

    public float immortalTime;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collider2D colliderToDisable;
    public Animator anim;
    public GameObject bullet;
    public PlayerMovement pMovement;

    public float jumpWhenHitted = 10f;

    public GameObject questionDeath;

    public bool canTeleport = false;
    bool canBeExecuted = true;

    //public PauseMenu pauseMenu;

    //bool jumpBack = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("MainChar-dead"))
        {
            bullet.SetActive(true);
        }

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (i == health && i == numOfHearts)
            {
                hearts[i].enabled = false;
            }
        }
        if (health <= 0)
        {
            pMovement.isDashing = false;
            pMovement.isMoving = false;

            rb.velocity = transform.up * 20f;

            StartCoroutine(QuestionBeforeDeath());
        }

        //if (jumpBack)
        //JumpBack();
    }
    //void JumpBack()
    //{
    //    minY = -22;

    //    if (transform.position.y <= minY)
    //    {
    //        StartCoroutine(TakePlayerDamage());
    //        rb.velocity = transform.up * m_Speed;
    //        anim.SetBool("IsJumping", true);
    //    }
    //}

    public Transform[] teleportPoints;
    public bool canTeleportInLevel = false;
    public IEnumerator QuestionBeforeDeath()
    {
        anim.SetTrigger("dead");
        colliderToDisable.enabled = false;

        yield return new WaitForSecondsRealtime(1.6f);

        if (!askedQuestion)
        {
            askedQuestion = true;
            questionDeath.SetActive(true);
            Cannon.canShoot = false;
            immortal = true;
            immortalTime = 3f;

            Transform playerPos = GameObject.Find("Player").transform;

            if (canTeleportInLevel)
            {
                if (teleportPoints.Length >= 2)
                {
                    Transform bossPos = GameObject.FindGameObjectWithTag("Boss").transform;

                    if (bossPos.position.x <= Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0.0f, 0.0f)).x)
                        playerPos.position = teleportPoints[1].position;
                    else if (bossPos.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0.0f, 0.0f)).x)
                        playerPos.position = teleportPoints[0].position;
                }
                else
                {
                    playerPos.position = teleportPoints[0].position;
                }
            }
        }
    }

    //IEnumerator Die()
    //{
    //    yield return new WaitForSecondsRealtime(2f);
    //    pauseMenu.StartCoroutine(pauseMenu.DeadMenu());
    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss") || other.CompareTag("BossBullet"))
        {
            StartCoroutine(TakePlayerDamage());
        }
    }

    public IEnumerator TakePlayerDamage()
    {
        //CameraShake shake = other.GetComponent<CameraShake>();
        if (!immortal)
        {
            immortal = true;

            if (health >= 1 && !immortalWhileDashing)
            {
                rb.velocity = transform.up * jumpWhenHitted;

                StartCoroutine(IndicateImmortal());
            }

            yield return new WaitForSeconds(immortalTime);

            immortalTime = 3f;

            immortal = false;
            immortalWhileDashing = false;
        }
    }

    private IEnumerator IndicateImmortal()
    {
        health--;

        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
