using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DarkEnemyBoss : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask playerLayer;//�v���C���[�ɓ���������U��

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
        if (!isCalledOnce)
        {
            isCalledOnce = true;
            animator.SetTrigger("Spawn");
        }


        rb.velocity = transform.right * -1 * MoveSpeed;

        timeSave += Time.deltaTime;

        //�o�ߎ��Ԃɉ����čU��
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

    //�G�̍U��������M�Y���ŕ\��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    //���񂾂Ƃ��̏���
    public void OnDamage(int damage)
    {
        HP -= damage;

        EvilBoss.BossHP -= 1;

        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
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
