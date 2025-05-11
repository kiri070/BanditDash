using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHaikei2 : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    //移動速度
    public float MoveSpeed;
    public float JumoForce;

    //時間管理
    public float Delay;
    private float TimeHave = 0f;

    private bool IsJump = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //右に移動
        if (IsJump)
        {
            animator.SetTrigger("Attack");
            rb.velocity = new Vector2(MoveSpeed, rb.velocity.y);
        }
        //ジャンプ
        else
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

        Invoke("Escape", 22f);
        Invoke("Death", 23f);

    }

    void Escape()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumoForce * 10f);
    }
    //オブジェクトを消す
    void Death()
    {
        Destroy(this.gameObject);
    }
}
