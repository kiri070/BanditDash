using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBoss : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    AudioSource audio;

    public AudioClip DeathSound;
    public AudioClip HakkyouSound;

    public static int BossHP = 7;
    private int Count = 0;
    private bool Life = true;

    //�ړ����x
    public float MoveSpeed;
    public float JumoForce;

    //���ԊǗ�
    public float Delay;
    private float TimeHave = 0f;

    private float DestroyDlay = 2f;
    private float DestroyTimeHave = 0f;

    private float HakkyouDelay = 36.2f;
    private float HakkyouTimeHave = 0f;


    private bool IsJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

      
    }

    // Update is called once per frame
    void Update()
    {
        //�E�Ɉړ�
        if (IsJump && Life)
        {
            animator.SetTrigger("Attack");
            rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
        }
        //�W�����v
        else if(!IsJump && Life)
        {
            animator.SetTrigger("Attack");
            rb.velocity = new Vector2(rb.velocity.x, JumoForce);
        }

        TimeHave += Time.deltaTime;

        if (TimeHave >= Delay)
        {
            IsJump = !IsJump;
            TimeHave = 0f;
        }

        if (BossHP == 6 && Count == 0)
        {
            animator.SetTrigger("Damaged");
            Count += 1;
        }

        else if (BossHP == 5 && Count == 1)
        {
            animator.SetTrigger("Damaged");
            Count += 1;
        }

        else if (BossHP == 4 && Count == 2)
        {
            animator.SetTrigger("Damaged");
            Count += 1;
        }

        else if (BossHP == 3 && Count == 3)
        {
            animator.SetTrigger("Damaged");
            Count += 1;
        }

        else if (BossHP == 2 && Count == 4)
        {
            animator.SetTrigger("Damaged");
            Count += 1;
        }

        else if (BossHP == 1 && Count == 5)
        {
            animator.SetTrigger("Damaged");
            Count += 1;
        }

        //�{�X�����񂾂Ƃ�
        if (BossHP == 0 && Count == 6)
        {
            audio.PlayOneShot(DeathSound, 0.5f);
            animator.SetTrigger("Death");
            rb.velocity = new Vector2(0f, 0f);
            Invoke("Destroy", 3.7f);
            Count += 1;

            Life = !Life;

            DestroyTimeHave = Time.time;

        }

        if(!Life &&  Time.time >= DestroyTimeHave + DestroyDlay)
        {
            rb.velocity = new Vector2(0, 2f);
        }

        HakkyouTimeHave += Time.deltaTime;
        //�|���؂�Ȃ������Ƃ�
        if(HakkyouTimeHave >= HakkyouDelay)
        {
            audio.PlayOneShot(HakkyouSound, 2f);
            Debug.Log("hakkyou");
            rb.velocity = new Vector2(MoveSpeed * -1 * 2, rb.velocity.y);
        }
    } 

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    
}
