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

    //�W�����v
    public float JumpForce = 20;
    private int JumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        lastJumpTime = Time.time; // ����̃W�����v���Ԃ�������
    }

    // Update is called once per frame
    void Update()
    {
        // �������Ȃ��悤�ɑ��x��ێ�
        rb.velocity = new Vector2(-MoveSpeed, rb.velocity.y);

        // 3�b���ƂɃW�����v���Ăяo��
        if (Time.time - lastJumpTime >= 2f)
        {
            JumpMovement();
            lastJumpTime = Time.time; // �Ō�̃W�����v���Ԃ��X�V
        }

    }

    void JumpMovement()
    {
        if (JumpCount < 1)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 0); // y�����̑��x�����Z�b�g���ăW�����v�ւ̉e�����Ȃ���
            rb.velocity += Vector2.up * JumpForce;//�W�����v
            JumpCount++;
        }
    }


    //���񂾂Ƃ��̏���
    public void OnDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //�v���C���[�ɐG�ꂽ���̏���
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
