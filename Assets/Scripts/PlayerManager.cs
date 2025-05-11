using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public GameObject hitEffectPrefab; // ��̃I�u�W�F�N�g��Prefab


    //�X�R�A
    private GameObject scoreText;
    const int MinScore = 0; //�X�R�A�̍ŏ��l
    public int ScoreBox = 0; //�_���[�W���󂯂����ɃX�R�A���v�Z���Ĉ�U�i�[���锠

    //�S�[���h
    private GameObject goldText;

    //�A�C�e��
    private GameObject potionText;
    



    //�o�[
    Slider slider;

    //���ʉ�
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


    //�{�����[��
    float DieVolumeScale = 0.3f;
    float DamageVolumeScele = 10f;
    float GameOverVolumeScele = 0.3f;

    //�v���C���[�Ɋւ������
    public float moveSpeed = 5f;
    public int PlayerHp = 3;
   
    public Transform attackPoint;
    public float attackRadius;
    public LayerMask enemyLayer;//�G�ɓ���������U��

    //Rigidbody2D��錾
    Rigidbody2D rb;

    Animator animator;

    int at = 1;//�v���C���[�̍U����

    //�W�����v
    public float JumpForce = 100f;
    private int JumpCount = 0;

    

    void Start()
    {
        //Stage4�̃{�XHP������
        EvilBoss.BossHP = 7;

        //ScoreText��T��
        scoreText = GameObject.Find("ScoreText");

        //GoldText��T��
        goldText = GameObject.Find("GoldText");

        //PotionText��T��
        potionText = GameObject.Find("PotionText");


        //���̃L��������Rigidbody2D���擾
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 1;



        // PlayerPrefs���璙�������[�h
        GoldManager.HaveGold = PlayerPrefs.GetInt("HAVEGOLD", 0);
        GoldManager.Gold = PlayerPrefs.GetInt("HAVEGOLD", 0);

        //�|�[�V�����������[�h
        PotionManager.Potion = PlayerPrefs.GetInt("HAVEPOTION", 0);
    }




    void Update()
    {
        //�J���p
        if (Input.GetKey(KeyCode.Space))
        {
            if (JumpCount < 1)
            {
                audioSource.PlayOneShot(JumpSound);
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, 0); // y�����̑��x�����Z�b�g���ăW�����v�ւ̉e�����Ȃ���
                rb.velocity += Vector2.up * JumpForce;//�W�����v
                JumpCount++;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            //�|�[�V���������銎�A�v���C���[��HP��3��菬�����Ƃ�
            if (PotionManager.Potion > 0 && PlayerHp < 3 && PlayerHp != 0)
            {
                audioSource.PlayOneShot(HealSound);

                PotionManager.Potion = Mathf.Max(PotionManager.Potion - 1, 0);

                PlayerHp = Mathf.Min(PlayerHp + 3, 3); //Mathf.Min(�����������I�������)

                slider.value += 1f;
            }

            else
            {
                //�|�[�V�������g���Ȃ��Ƃ�
                audioSource.PlayOneShot(CannotUseSound);
            }
        }

        Movement();
       
            //�v���C���[�����񂾂Ƃ�
            if (PlayerHp <= 0)
            {
              
                audioSource.PlayOneShot(DieSound, DieVolumeScale);
                audioSource.PlayOneShot(GameOver, GameOverVolumeScele);
                animator.SetTrigger("Die");
                rb.velocity = new Vector2(0, 0);

                //delay�����ăV�[���ϑJ
                Invoke("LoadScene_Game_Death", 3.1f);
               
            }

        //Stege4�̃{�X�����񂾂Ƃ�
        if (EvilBoss.BossHP == 0 && SceneManager.GetActiveScene().name == "Stage4")
        {
            Invoke("Stage4Clear", 3.9f);
        }

    }


    //�U�������Ƃ��̏���
    void Attack()
    {
        audioSource.PlayOneShot(SwordSound);
        animator.SetTrigger("IsAttack");
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        //Game�V�[���̎�
        if (SceneManager.GetActiveScene().name == "Game")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffect���\�b�h�̌Ăяo��
                ShowHitEffect(hitEnemy.transform.position); //�U�������������G�̈ʒu


                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);
                
                hitEnemy.GetComponent<EnemyManager>().OnDamage(at);


                //�X�R�A���Z
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //�S�[���h
                GoldManager.HaveGold += 30; //����

                GoldManager.Gold += 30; //�Q�[�����̃S�[���h


            }
        }

        //Stage2�V�[���̎�
        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffect���\�b�h�̌Ăяo��
                ShowHitEffect(hitEnemy.transform.position);�@//�U�������������G�̈ʒu

                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);
                
                // �I�u�W�F�N�g���Ƃɏ����𕪊�
                if (hitEnemy.GetComponent<Octopus>() != null)
                {
                    hitEnemy.GetComponent<Octopus>().OnDamage(at);
                }
                else if (hitEnemy.GetComponent<JumperEnemy>() != null)
                {
                    hitEnemy.GetComponent<JumperEnemy>().OnDamage(at);
                }


                //�X�R�A���Z
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //�S�[���h
                GoldManager.HaveGold += 30; //����

                GoldManager.Gold += 30; //�Q�[�����̃S�[���h


            }
        }

        //Stage3�V�[���̎�
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffect���\�b�h�̌Ăяo��
                ShowHitEffect(hitEnemy.transform.position); //�U�������������G�̈ʒu


                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);

                

                // �I�u�W�F�N�g���Ƃɏ����𕪊�
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

                //�X�R�A���Z
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //�S�[���h
                GoldManager.HaveGold += 30; //����

                GoldManager.Gold += 30; //�Q�[�����̃S�[���h


            }
        }

        //Stage3�V�[���̎�
        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            foreach (Collider2D hitEnemy in hitEnemys)
            {
                //ShowHitEffect���\�b�h�̌Ăяo��
                ShowHitEffect(hitEnemy.transform.position); //�U�������������G�̈ʒu


                audioSource.PlayOneShot(KillSound);
                audioSource.PlayOneShot(KillSwordSound, 2f);



                // �I�u�W�F�N�g���Ƃɏ����𕪊�
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

                //�X�R�A���Z
                scoreText.GetComponent<ScoreManager>().score += 5000;

                //�S�[���h
                GoldManager.HaveGold += 30; //����

                GoldManager.Gold += 30; //�Q�[�����̃S�[���h


            }
        }
    }



    // �G�t�F�N�g��\�����郁�\�b�h
    void ShowHitEffect(Vector3 position)
    {
        // �G�t�F�N�g�̈ʒu��������ɂ��炷
        position += new Vector3(0, 0.3f, 0);

        // ��̃I�u�W�F�N�g�𐶐�
        GameObject hitEffect = Instantiate(hitEffectPrefab, position, Quaternion.identity);

        // �A�N�e�B�u�ɂ��ăG�t�F�N�g�Đ�
        hitEffect.SetActive(true);

        // ��A�N�e�B�u�ɂ��邽�߂̃R���[�`�����J�n
        StartCoroutine(DeactivateEffect(hitEffect));
    }

    // �G�t�F�N�g���A�N�e�B�u�ɂ���R���[�`��
    IEnumerator DeactivateEffect(GameObject effect)
    {
        // �G�t�F�N�g�Đ���A���b�҂��Ă����A�N�e�B�u�ɂ���
        yield return new WaitForSeconds(0.3f);

        // ��A�N�e�B�u�ɂ���
        effect.SetActive(false);
    }





    //�v���C���[�̍U��������M�Y���ŕ\��
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    //����G����U�����󂯂����̃_���[�W
    public void OnDamaged(int damage)
    {
        PlayerHp -= damage;
        audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
        animator.SetTrigger("Damage");
        slider.value -= 0.4f;

        //�X�R�A�̌v�Z
        ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;
    }

    //�v���C���[�̓���
    void Movement()
    {
        // �������Ȃ��悤�ɑ��x��ێ�
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    //���񂾂Ƃ��Ƀf�X��ʂɕϑJ
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

    //�N���A�����Ƃ��ɃN���A��ʂɕϑJ
    void LoadScene_Stage1Clear()
    {
        SceneManager.LoadScene("Stage1Clear");
    }

    void LoadScene_Stage2Clear()
    {
        SceneManager.LoadScene("Stage2Clear");
    }

    //Stage4���N���A�����Ƃ�
    void Stage4Clear()
    {
        SceneManager.LoadScene("Stage4Clear");
    }

    

    //�Փ˂����Ƃ��̏���
    void OnCollisionEnter2D(Collision2D other)
    {
        //�G�ɓ����������̏���
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
            animator.SetTrigger("Damage");
            PlayerHp -= 1;

            slider.value -= 0.4f;

            //�{�X������̖��G����
            if (SceneManager.GetActiveScene().name == "Stage4" && EvilBoss.BossHP == 0)
            {
                PlayerHp += 1;
            }

            //�v���C���[��HP��1�ȏ�Ȃ�
            if (PlayerHp >= 1)
            {
                //�X�R�A�̌v�Z
                ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;

                //�X�R�A��0�ȉ���������
                if (ScoreBox <= MinScore)
                {
                    scoreText.GetComponent<ScoreManager>().score = MinScore;
                }

                //�X�R�A��0�ȏ�̎�
                else
                {
                    scoreText.GetComponent<ScoreManager>().score = ScoreBox;
                }
            }
            

        }

        //�{�X�ɐG�ꂽ��
        if (other.gameObject.CompareTag("Boss") && EvilBoss.BossHP >= 1)
        {
            PlayerHp -= 3;
            slider.value -= 1f;

            //�X�R�A�̌v�Z
            ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;

            //�X�R�A��0�ȉ���������
            if (ScoreBox <= MinScore)
            {
                scoreText.GetComponent<ScoreManager>().score = MinScore;
            }

            //�X�R�A��0�ȏ�̎�
            else
            {
                scoreText.GetComponent<ScoreManager>().score = ScoreBox;
            }
        }

        //�W�����v�̔���
        if (other.gameObject.CompareTag("Floor_Stone"))
        {
            JumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        //�X�^�b�N�h�~�̓����̕ǂɐG�ꂽ��
        if (other.gameObject.CompareTag("DeathHantei"))
        {
            audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
            animator.SetTrigger("Damage");
            PlayerHp -= 3;

            slider.value -= 1f;

            //�X�R�A�̌v�Z
            ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;



            //�X�R�A��0�ȉ���������
            if (ScoreBox <= MinScore)
            {
                scoreText.GetComponent<ScoreManager>().score = MinScore;
            }

            //�X�R�A��0�ȏ�̎�
            else
            {
                scoreText.GetComponent<ScoreManager>().score = ScoreBox;
            }
        }

        //�g���b�v�ɐG�ꂽ��
        if (other.gameObject.CompareTag("DamageTrap"))
        {
            audioSource.PlayOneShot(DamageSound, DamageVolumeScele);
            animator.SetTrigger("Damage");


            //�{�X������̖��G����
            if (SceneManager.GetActiveScene().name == "Stage4" && EvilBoss.BossHP == 0)
            {
                PlayerHp += 1;
            }

            PlayerHp -= 1;
            slider.value -= 0.4f;

            if (PlayerHp >= 1)
            {
                //�X�R�A�̌v�Z
                ScoreBox = scoreText.GetComponent<ScoreManager>().score - 5000;

                //�X�R�A��0�ȉ���������
                if (ScoreBox <= MinScore)
                {
                    scoreText.GetComponent<ScoreManager>().score = MinScore;
                }

                //�X�R�A��0�ȏ�̎�
                else
                {
                    scoreText.GetComponent<ScoreManager>().score = ScoreBox;

                }
            }
            
        }

        //�񕜃A�C�e���������
        if (other.gameObject.CompareTag("LifeHeal"))
        {
            audioSource.PlayOneShot(HealSound);
            PlayerHp = Mathf.Min(PlayerHp + 1, 3); //Mathf.Min(�����������I�������)

            slider.value += 0.4f;
        }

        //�񕜃|�[�V�������������
        if (other.gameObject.CompareTag("MaxHeal"))
        {
            audioSource.PlayOneShot(HealSound);
            PlayerHp = Mathf.Min(PlayerHp + 3, 3);�@//Mathf.Min(�����������I�������)

            slider.value += 1f;
        }

        //�R�C�����������
        if (other.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(CoinSound);
            //�X�R�A���Z
            scoreText.GetComponent<ScoreManager>().score += 200;

            //�S�[���h
            GoldManager.HaveGold += 1; //����

            GoldManager.Gold += 1; //�Q�[�����̃S�[���h

           

        }

        //�_�C�������h���������
        if (other.gameObject.CompareTag("Diamond"))
        {
            audioSource.PlayOneShot(DiamondSound);
            //�X�R�A���Z
            scoreText.GetComponent<ScoreManager>().score += 1000;

            //�S�[���h
            GoldManager.HaveGold += 10; //����

            GoldManager.Gold += 10; //�Q�[�����̃S�[���h

        }


        //�S�[���̃`�F�X�g�ɐG�ꂽ��
        if (other.gameObject.CompareTag("Chest"))
        {
            animator.SetTrigger("Clear"); //�A�C�h���ɂȂ�

            if(SceneManager.GetActiveScene().name == "Game")
            {
                //�X�R�A���Z
                scoreText.GetComponent<ScoreManager>().score += 100000;

                //�S�[���h
                GoldManager.HaveGold += 500; //����

                GoldManager.Gold += 500; //�Q�[�����̃S�[���h

                //delay�����ăV�[���ϑJ
                Invoke("LoadScene_Stage1Clear", 1.3f);
            }

            else if (SceneManager.GetActiveScene().name == "Stage2")
            {
                //�X�R�A���Z
                scoreText.GetComponent<ScoreManager>().score += 100000;

                //�S�[���h
                GoldManager.HaveGold += 500; //����

                GoldManager.Gold += 500; //�Q�[�����̃S�[���h

                //delay�����ăV�[���ϑJ
                Invoke("LoadScene_Stage2Clear", 1.3f);
            }

        }
    }

    //�W�����v�{�^��
    public void JumpButton()
    {
        if (JumpCount < 1)
        {
            audioSource.PlayOneShot(JumpSound);
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, 0); // y�����̑��x�����Z�b�g���ăW�����v�ւ̉e�����Ȃ���
            rb.velocity += Vector2.up * JumpForce;//�W�����v
            JumpCount++;
        }

    }

    //�U���{�^��
    public void AttackButton()
    {
        Attack();
    }

    //�񕜃{�^��
    public void HealButton()
    {
        //�|�[�V���������銎�A�v���C���[��HP��3��菬�����Ƃ�
        if (PotionManager.Potion > 0 && PlayerHp < 3 && PlayerHp != 0)
        {
            audioSource.PlayOneShot(HealSound);

            PotionManager.Potion = Mathf.Max(PotionManager.Potion - 1, 0);

            PlayerHp = Mathf.Min(PlayerHp + 3, 3); //Mathf.Min(�����������I�������)

            slider.value += 1f;
        }

        else
        {
            //�|�[�V�������g���Ȃ��Ƃ�
            audioSource.PlayOneShot(CannotUseSound);
        }
    }
}


   