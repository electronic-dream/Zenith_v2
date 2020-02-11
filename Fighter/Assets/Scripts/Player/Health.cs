using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Health : MonoBehaviour
{
    public static int health = 1;
    public int numOfHearts;
    [SerializeField]
    public bool immortal = false;

    //public float speed;
    //public float minX;
    //public float maxX;
    //public float playerPos;
    //public float m_Speed;

    public float immortalTime;
    public float minY;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Transform player;
    public Collider2D colliderToDisable;
    public Animator anim;
    public GameObject bullet;
    public PlayerMovement pMovement;
    public float jumpWhenHitted = 10f;

    public GameObject questionDeath;
    private static int count = 0;

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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("MainCharDead"))
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
            rb.velocity = transform.up * 20f;

            pMovement.isDashing = false;
            pMovement.isMoving = false;

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

    public IEnumerator QuestionBeforeDeath()
    {
        if (count > 0)
        {
            Dead();
        }

        yield return new WaitForSecondsRealtime(1.6f);

        if (count == 0)
        {
            questionDeath.SetActive(true);

            count++;
        }
    }

    public void Dead()
    {
        colliderToDisable.enabled = false;
        //jumpBack = false;

        anim.SetBool("dead", true);
        anim.SetBool("IsJumping", false);

        bullet.SetActive(false);

        //StartCoroutine(Die());
        //Destroy(gameObject, 2f);
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
            health--;
            //numOfHearts--;

            immortal = true;
            //noDmg = true;

            rb.velocity = transform.up * jumpWhenHitted;

            if (health >= 1)
            {
                StartCoroutine(IndicateImmortal());
            }

            yield return new WaitForSeconds(immortalTime);

            immortal = false;
            //noDmg = false;
        }
    }

    private IEnumerator IndicateImmortal()
    {
        //Debug.Log("Inicate Immortal");
        while (immortal)
        {
            //Debug.Log("I");
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.3f);

            //Debug.Log("I2");
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.3f);
        }
    }
}
