using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    Animator animator;

    Rigidbody2D rb;
    public float MoveSpeed = 2;

    public int HP = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //���Ɉړ���������
        rb.velocity = transform.right * -1 * MoveSpeed;
    
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
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
