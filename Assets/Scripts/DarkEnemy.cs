using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnemy : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask playerLayer;//プレイヤーに当たったら攻撃

    public float timeOut = 2f;
    private float timeSave = 0.0f;

    Rigidbody2D rb;
    Animator animator;

    bool isCalledOnce = false;

    public int HP = 1;

    public float MoveSpeed = 3;

    public int Attack = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!isCalledOnce)
        {
            isCalledOnce = true;
            animator.SetTrigger("Spawn");
        }
       

        rb.velocity = transform.right * -1 * MoveSpeed;

        timeSave += Time.deltaTime;

        //経過時間に応じて攻撃
        if (timeSave >= timeOut)
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, playerLayer);
            foreach (Collider2D PlayerCollider in hitPlayers)
            {
                PlayerCollider.GetComponent<PlayerManager>().OnDamaged(Attack);
            }

            timeSave = 0.0f;
        }
    }

    //敵の攻撃判定をギズモで表示
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
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
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
