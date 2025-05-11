using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public GameObject hitEffectPrefab; // 空のオブジェクトのPrefab


    //スコア
    private GameObject scoreText;
    const int MinScore = 0; //スコアの最小値
    public int ScoreBox = 0; //ダメージを受けた時にスコアを計算して一旦格納する箱

    //ゴールド
    private GameObject goldText;

    //アイテム
    private GameObject potionText;
    



    //バー
    Slider slider;

    //効果音
    AudioSource audioSource;
    public AudioClip DieSound;
    public AudioClip DamageSound;
    public AudioClip JumpSound;
    public AudioClip KillSound;
    public AudioClip KillSwordSound;
    public AudioClip GameOver;
    public AudioClip SwordSound;
    public AudioClip HealSound;
    public AudioClip CoinSound;
    public AudioClip DiamondSound;
    public AudioClip CannotUseSound;


    //ボリューム
    float DieVolumeScale = 0.3f;
    float DamageVolumeScele = 10f;
    float GameOverVolumeScele = 0.3f;

    //プレイヤーに関するもの
    public float moveSpeed = 5f;
    public int PlayerHp = 3;
   
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;//敵に当たったら攻撃

    //Rigidbody2Dを宣言
    Rigidbody2D rb;

    Animator animator;

    int at = 1;//プレイヤーの攻撃力

    //ジャンプ
    public float JumpForce = 100f;
    private int JumpCount = 0;

    

    void Start()
    {
        //Stage4のボスHP初期化
        EvilBoss.BossHP = 7;

        //ScoreTextを探す
        scoreText = GameObject.Find("ScoreText");

        //GoldTextを探す
        goldText = GameObject.Find("GoldText");

        //PotionTextを探す
        potionText = GameObject.Find("PotionText");


        //このキャラからRigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 1;



        // PlayerPrefsから貯金をロード
        GoldManager.HaveGold = PlayerPrefs.GetInt("HAVEGOLD", 0);
        GoldManager.Gold = PlayerPrefs.GetInt("HAVEGOLD", 0);

        //ポーション数をロード
        PotionManager.Potion = PlayerPrefs.GetInt("HAVEPOTION", 0);
    }




    void Update()
    {
        //開発用
        if (Input.GetKey(KeyCode.Space))
        {
            if (JumpCount < 1)
            {
                audioSource.PlayOneShot(JumpSound);
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, 0); // y方向の速度をリセットしてジャンプへの影響をなくす
                rb.velocity += Vector2.up * JumpForce;//ジャンプ
                JumpCount++;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            //ポーションがある且つ、プレイヤーのHPが3より小さいとき
            if (PotionManager.Potion > 0 && PlayerHp < 3 && PlayerHp != 0)
            {
                audioSource.PlayOneShot(HealSound);

                PotionManager.Potion = Mathf.Max(PotionManager.Potion - 1, 0);

                PlayerHp = Mathf.Min(PlayerHp + 3, 3); //Mathf.Min(小さい方が選択される)

                slider.value += 1f;
            }

            else
            {
                //ポーションを使えないとき
                audioSource.PlayOneShot(CannotUseSound);
            }
        }

        Movement();
       
            //プレイヤーが死んだとき
            if (PlayerHp <= 0)
            {
              
                audioSource.PlayOneShot(DieSound, DieVolumeScale);
                audioSource.PlayOneShot(GameOver, GameOverVolumeScele);
                animator.SetTrigger("Die");
                rb.velocity = new Vector2(0, 0);

                //delayを入れてシーン変遷
                Invoke("LoadScene_Game_Death", 3.1f);
               
            }

        //Stege4のボスが死んだとき
        if (EvilBoss.BossHP == 0 && SceneManager.GetActiveScene().name == "Stage4")
        {
            Invoke("Stage4Clear", 3.9f);
        }

    }


    //攻撃したときの処理
    void Attack()
    {
        audioSource.PlayOneShot(SwordSound);
        animator.SetTrigger("IsAttack");
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        //Gameシーンの時
        if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffectメソッドの呼び出し
                ShowHitEffect(hitEnemy.transform.position); //攻撃が当たった敵の位置


                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);
                
                hitEnemy.GetComponent<EnemyManager>().OnDamage(at);


                //スコア換算
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //ゴールド
                GoldManager.HaveGold += 30; //貯金

                GoldManager.Gold += 30; //ゲーム中のゴールド


            }
        }

        //Stage2シーンの時
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffectメソッドの呼び出し
                ShowHitEffect(hitEnemy.transform.position);　//攻撃が当たった敵の位置

                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);
                
                // オブジェクトごとに処理を分岐
                if (hitEnemy.GetComponent<Octopus>() != null)
                {
                    hitEnemy.GetComponent<Octopus>().OnDamage(at);
                }
                else if (hitEnemy.GetComponent<JumperEnemy>() != null)
                {
                    hitEnemy.GetComponent<JumperEnemy>().OnDamage(at);
                }


                //スコア換算
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //ゴールド
                GoldManager.HaveGold += 30; //貯金

                GoldManager.Gold += 30; //ゲーム中のゴールド


            }
        }

        //Stage3シーンの時
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffectメソッドの呼び出し
                ShowHitEffect(hitEnemy.transform.position); //攻撃が当たった敵の位置


                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);

                

                // オブジェクトごとに処理を分岐
                if (hitEnemy.GetComponent<DarkEnemy>() != null)
                {
                    hitEnemy.GetComponent<DarkEnemy>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<EnemyManager>() != null)
                {
                    hitEnemy.GetComponent<EnemyManager>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<JumperEnemy>() != null)
                {
                    hitEnemy.GetComponent<JumperEnemy>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<Octopus>() != null)
                {
                    hitEnemy.GetComponent<Octopus>().OnDamage(at);
                }

                //スコア換算
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //ゴールド
                GoldManager.HaveGold += 30; //貯金

                GoldManager.Gold += 30; //ゲーム中のゴールド


            }
        }

        //Stage3シーンの時
        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffectメソッドの呼び出し
                ShowHitEffect(hitEnemy.transform.position); //攻撃が当たった敵の位置


                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);



                // オブジェクトごとに処理を分岐
                if (hitEnemy.GetComponent<DarkEnemy>() != null)
                {
                    hitEnemy.GetComponent<DarkEnemy>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<EnemyManager>() != null)
                {
                    hitEnemy.GetComponent<EnemyManager>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<JumperEnemy>() != null)
                {
                    hitEnemy.GetComponent<JumperEnemy>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<Octopus>() != null)
                {
                    hitEnemy.GetComponent<Octopus>().OnDamage(at);
                }

                else if (hitEnemy.GetComponent<DarkEnemyBoss>() != null)
                {
                    hitEnemy.GetComponent<DarkEnemyBoss>().OnDamage(at);
                }

                //スコア換算
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //ゴールド
                GoldManager.HaveGold += 30; //貯金

                GoldManager.Gold += 30; //ゲーム中のゴールド


            }
        }
    }



    // エフェクトを表示するメソッド
    void ShowHitEffect(Vector3 position)
    {
        // エフェクトの位置を少し上にずらす
        position += new Vector3(0, 0.3f, 0);

        // 空のオブジェクトを生成
        GameObject hitEffect = Instantiate(hitEffectPrefab, position, Quaternion.identity);

        // アクティブにしてエフェクト再生
        hitEffect.SetActive(true);

        // 非アクティブにするためのコルーチンを開始
        StartCoroutine(DeactivateEffect(hitEffect));
    }

    // エフェクトを非アクティブにするコルーチン
    IEnumerator DeactivateEffect(GameObject effect)
    {
        // エフェクト再生後、数秒待ってから非アクティブにする
        yield return new WaitForSeconds(0.3f);

        // 非アクティブにする
        effect.SetActive(false);
    }





    //プレイヤーの攻撃判定をギズモで表示
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    //ある敵から攻撃を受けた時のダメージ
    public void OnDamaged(int damage)
    {
        PlayerHp -= damage;
        audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
        animator.SetTrigger("Damage");
        slider.value -= 0.4f;

        //スコアの計算
        ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;
    }

    //プレイヤーの動き
    void Movement()
    {
        // 加速しないように速度を保持
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    //死んだときにデス画面に変遷
    void LoadScene_Game_Death()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            SceneManager.LoadScene("Game_Death");
        }
        

        if(SceneManager.GetActiveScene().name == "Stage2")
        {
            SceneManager.LoadScene("Stage2_Death");
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            SceneManager.LoadScene("Stage3_Death");
        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            SceneManager.LoadScene("Stage4_Death");
        }
    }

    //クリアしたときにクリア画面に変遷
    void LoadScene_Stage1Clear()
    {
        SceneManager.LoadScene("Stage1Clear");
    }

    void LoadScene_Stage2Clear()
    {
        SceneManager.LoadScene("Stage2Clear");
    }

    //Stage4をクリアしたとき
    void Stage4Clear()
    {
        SceneManager.LoadScene("Stage4Clear");
    }

    

    //衝突したときの処理
    void OnCollisionEnter2D(Collision2D other)
    {
        //敵に当たった時の処理
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
            animator.SetTrigger("Damage");
            PlayerHp -= 1;

            slider.value -= 0.4f;

            //ボス討伐後の無敵時間
            if (SceneManager.GetActiveScene().name == "Stage4" && EvilBoss.BossHP == 0)
            {
                PlayerHp += 1;
            }

            //プレイヤーのHPが1以上なら
            if (PlayerHp >= 1)
            {
                //スコアの計算
                ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;

                //スコアが0以下だった時
                if (ScoreBox <= MinScore)
                {
                    scoreText.GetComponent<ScoreManager>().score = MinScore;
                }

                //スコアが0以上の時
                else
                {
                    scoreText.GetComponent<ScoreManager>().score = ScoreBox;
                }
            }
            

        }

        //ボスに触れた時
        if (other.gameObject.CompareTag("Boss") && EvilBoss.BossHP >= 1)
        {
            PlayerHp -= 3;
            slider.value -= 1f;

            //スコアの計算
            ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;

            //スコアが0以下だった時
            if (ScoreBox <= MinScore)
            {
                scoreText.GetComponent<ScoreManager>().score = MinScore;
            }

            //スコアが0以上の時
            else
            {
                scoreText.GetComponent<ScoreManager>().score = ScoreBox;
            }
        }

        //ジャンプの判定
        if (other.gameObject.CompareTag("Floor_Stone"))
        {
            JumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //スタック防止の透明の壁に触れた時
        if (other.gameObject.CompareTag("DeathHantei"))
        {
            audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
            animator.SetTrigger("Damage");
            PlayerHp -= 3;

            slider.value -= 1f;

            //スコアの計算
            ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;



            //スコアが0以下だった時
            if (ScoreBox <= MinScore)
            {
                scoreText.GetComponent<ScoreManager>().score = MinScore;
            }

            //スコアが0以上の時
            else
            {
                scoreText.GetComponent<ScoreManager>().score = ScoreBox;
            }
        }

        //トラップに触れた時
        if (other.gameObject.CompareTag("DamageTrap"))
        {
            audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
            animator.SetTrigger("Damage");


            //ボス討伐後の無敵時間
            if (SceneManager.GetActiveScene().name == "Stage4" && EvilBoss.BossHP == 0)
            {
                PlayerHp += 1;
            }

            PlayerHp -= 1;
            slider.value -= 0.4f;

            if (PlayerHp >= 1)
            {
                //スコアの計算
                ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;

                //スコアが0以下だった時
                if (ScoreBox <= MinScore)
                {
                    scoreText.GetComponent<ScoreManager>().score = MinScore;
                }

                //スコアが0以上の時
                else
                {
                    scoreText.GetComponent<ScoreManager>().score = ScoreBox;

                }
            }
            
        }

        //回復アイテム取った時
        if (other.gameObject.CompareTag("LifeHeal"))
        {
            audioSource.PlayOneShot(HealSound);
            PlayerHp = Mathf.Min(PlayerHp + 1, 3); //Mathf.Min(小さい方が選択される)

            slider.value += 0.4f;
        }

        //回復ポーションを取った時
        if (other.gameObject.CompareTag("MaxHeal"))
        {
            audioSource.PlayOneShot(HealSound);
            PlayerHp = Mathf.Min(PlayerHp + 3, 3);　//Mathf.Min(小さい方が選択される)

            slider.value += 1f;
        }

        //コインを取った時
        if (other.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(CoinSound);
            //スコア換算
            scoreText.GetComponent<ScoreManager>().score += 200;

            //ゴールド
            GoldManager.HaveGold += 1; //貯金

            GoldManager.Gold += 1; //ゲーム中のゴールド

           

        }

        //ダイヤモンドを取った時
        if (other.gameObject.CompareTag("Diamond"))
        {
            audioSource.PlayOneShot(DiamondSound);
            //スコア換算
            scoreText.GetComponent<ScoreManager>().score += 1000;

            //ゴールド
            GoldManager.HaveGold += 10; //貯金

            GoldManager.Gold += 10; //ゲーム中のゴールド

        }


        //ゴールのチェストに触れた時
        if (other.gameObject.CompareTag("Chest"))
        {
            animator.SetTrigger("Clear"); //アイドルになる

            if(SceneManager.GetActiveScene().name == "Game")
            {
                //スコア換算
                scoreText.GetComponent<ScoreManager>().score += 100000;

                //ゴールド
                GoldManager.HaveGold += 500; //貯金

                GoldManager.Gold += 500; //ゲーム中のゴールド

                //delayを入れてシーン変遷
                Invoke("LoadScene_Stage1Clear", 1.3f);
            }

            else if (SceneManager.GetActiveScene().name == "Stage2")
            {
                //スコア換算
                scoreText.GetComponent<ScoreManager>().score += 100000;

                //ゴールド
                GoldManager.HaveGold += 500; //貯金

                GoldManager.Gold += 500; //ゲーム中のゴールド

                //delayを入れてシーン変遷
                Invoke("LoadScene_Stage2Clear", 1.3f);
            }

        }
    }

    //ジャンプボタン
    public void JumpButton()
    {
        if (JumpCount < 1)
        {
            audioSource.PlayOneShot(JumpSound);
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 0); // y方向の速度をリセットしてジャンプへの影響をなくす
            rb.velocity += Vector2.up * JumpForce;//ジャンプ
            JumpCount++;
        }

    }

    //攻撃ボタン
    public void AttackButton()
    {
        Attack();
    }

    //回復ボタン
    public void HealButton()
    {
        //ポーションがある且つ、プレイヤーのHPが3より小さいとき
        if (PotionManager.Potion > 0 && PlayerHp < 3 && PlayerHp != 0)
        {
            audioSource.PlayOneShot(HealSound);

            PotionManager.Potion = Mathf.Max(PotionManager.Potion - 1, 0);

            PlayerHp = Mathf.Min(PlayerHp + 3, 3); //Mathf.Min(小さい方が選択される)

            slider.value += 1f;
        }

        else
        {
            //ポーションを使えないとき
            audioSource.PlayOneShot(CannotUseSound);
        }
    }
}


   