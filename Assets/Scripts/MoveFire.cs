using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFire : MonoBehaviour
{
    Rigidbody2D rb;
    public float MoveSpeed;　//スピード
    public float FloatForce; //浮上力

    //音
    AudioSource audio;
    public AudioClip FireSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 0f, 90f); //オブジェクトの向き
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(FireSound);
    }

    // Update is called once per frame
    void Update()
    {
        //動きの処理
        rb.velocity = transform.right * -1 * MoveSpeed;
        rb.velocity = transform.up * FloatForce;

        Invoke("Destroy", 7f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーに触れたら消去
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
