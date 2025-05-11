using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHaikei : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    //移動速度
    public float MoveSpeed = 5;

    public float timeOut = 2f; //遅延する時間
    private float timeElapsed = 0.0f; //経過時間を保存する箱

    private bool isMovingRight = true;
    private Vector3 defaultScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        defaultScale = transform.localScale; //初期のスケールを保存
    }

    // Update is called once per frame
    void Update()
    {
        //右に移動
        if (isMovingRight)
        {
            rb.velocity = transform.right * MoveSpeed * 2.3f;
            transform.localScale = defaultScale; //キャラを右にする
        }
        //左に移動
        else
        {
            animator.SetTrigger("Attack");
            rb.velocity = transform.right * -1 * MoveSpeed;
            transform.localScale = new Vector3(-defaultScale.x, defaultScale.y, defaultScale.z);
        }

        timeElapsed += Time.deltaTime;　//経過時間を保存

        //timeOut変数の数より時間が経過していたら方向を戻す
        if (timeElapsed >= timeOut)
        {
            isMovingRight = !isMovingRight; // 方向を切り替える
            timeElapsed = 0.0f;
        }


        Invoke("Death", 12);

    }

    //オブジェクトを消す
    void Death()
    {
        Destroy(this.gameObject);
    }
}
