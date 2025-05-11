using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int hp = 1;
    Animator animator;
    Rigidbody2D rb;

    public float moveSpeed = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //���Ɉړ���������
        rb.velocity = transform.right * -1 * moveSpeed;
    }

    //�_���[�W���󂯂����̏���
    public void OnDamage(int damage)
    {
        hp -= damage;
        animator.SetTrigger("IsHurt");

        if(hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        hp = 0;
        animator.SetTrigger("Die");
        
        Destroy(this.gameObject);
    }

    //�v���C���[�ɐG�ꂽ���̏���
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
