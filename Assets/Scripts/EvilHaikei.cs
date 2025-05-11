using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHaikei : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    //�ړ����x
    public float MoveSpeed = 5;

    public float timeOut = 2f; //�x�����鎞��
    private float timeElapsed = 0.0f; //�o�ߎ��Ԃ�ۑ����锠

    private bool isMovingRight = true;
    private Vector3 defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defaultScale = transform.localScale; //�����̃X�P�[����ۑ�
    }

    // Update is called once per frame
    void Update()
    {
        //�E�Ɉړ�
        if (isMovingRight)
        {
            rb.velocity = transform.right * MoveSpeed * 2.3f;
            transform.localScale = defaultScale; //�L�������E�ɂ���
        }
        //���Ɉړ�
        else
        {
            animator.SetTrigger("Attack");
            rb.velocity = transform.right * -1 * MoveSpeed;
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }

        timeElapsed += Time.deltaTime;�@//�o�ߎ��Ԃ�ۑ�

        //timeOut�ϐ��̐���莞�Ԃ��o�߂��Ă����������߂�
        if (timeElapsed >= timeOut)
        {
            isMovingRight = !isMovingRight; // ������؂�ւ���
            timeElapsed = 0.0f;
        }


        Invoke("Death", 12);

    }

    //�I�u�W�F�N�g������
    void Death()
    {
        Destroy(this.gameObject);
    }
}
