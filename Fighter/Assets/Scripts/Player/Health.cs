using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    [SerializeField]
    public bool immortal = false;
    public bool noDmg = false;

    //public float speed;
    //public float minX;
    //public float maxX;
    //public float playerPos;
    //public float m_Speed;

    [SerializeField]
    private float immortalTime;
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
    public float jumpWhenHitted = 10f;

    //public PauseMenu pauseMenu;

    //bool jumpBack = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            Dead();
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
    public void Dead()
    {
        colliderToDisable.enabled = false;
        //jumpBack = false;

        anim.SetBool("dead", true);
        anim.SetBool("IsJumping", false);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("MainCharDead"))
        {
            bullet.SetActive(false);
        }

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
        if (other.CompareTag("Boss"))
        {
            StartCoroutine(TakePlayerDamage());
        }
    }

    public IEnumerator TakePlayerDamage()
    {
        //CameraShake shake = other.GetComponent<CameraShake>();
        if (!immortal || !noDmg)
        {
            health--;
            //numOfHearts--;

            immortal = true;
            noDmg = true;

            rb.velocity = transform.up * jumpWhenHitted;

            if (health > 0)
            {
                StartCoroutine(IndicateImmortal());
            }

            yield return new WaitForSeconds(immortalTime);

            immortal = false;
            noDmg = false;
        }
    }
    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
