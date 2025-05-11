using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperEnemy : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    public float MoveSpeed = 2;

    public int HP = 1;

    private float lastJumpTime;

    //ジャンプ
    public float JumpForce = 20;
    private int JumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        lastJumpTime = Time.time; // 初回のジャンプ時間を初期化
    }

    // Update is called once per frame
    void Update()
    {
        // 加速しないように速度を保持
        rb.velocity = new Vector2(-MoveSpeed, rb.velocity.y);

        // 3秒ごとにジャンプを呼び出す
        if (Time.time - lastJumpTime >= 2f)
        {
            JumpMovement();
            lastJumpTime = Time.time; // 最後のジャンプ時間を更新
        }

    }

    void JumpMovement()
    {
        if (JumpCount < 1)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 0); // y方向の速度をリセットしてジャンプへの影響をなくす
            rb.velocity += Vector2.up * JumpForce;//ジャンプ
            JumpCount++;
        }
    }


    //死んだときの処理
    public void OnDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //プレイヤーに触れた時の処理
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor_Stone"))
        {
            JumpCount = 0;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
