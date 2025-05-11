using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFire : MonoBehaviour
{
    Rigidbody2D rb;
    public float MoveSpeed;�@//�X�s�[�h
    public float FloatForce; //�����

    //��
    AudioSource audio;
    public AudioClip FireSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 0f, 90f); //�I�u�W�F�N�g�̌���
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(FireSound);
    }

    // Update is called once per frame
    void Update()
    {
        //�����̏���
        rb.velocity = transform.right * -1 * MoveSpeed;
        rb.velocity = transform.up * FloatForce;

        Invoke("Destroy", 7f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //�v���C���[�ɐG�ꂽ�����
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
