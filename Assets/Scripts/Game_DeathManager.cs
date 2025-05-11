using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_DeathManager : MonoBehaviour
{
    Animator animator;
    public Animator heavyBanditAnimator; // HeavyBandit_0のAnimatorをInspectorからアタッチする

    //効果音
    AudioSource audioSource;
    public AudioClip Hukkatu;
    public AudioClip Hukkatu2;
    public AudioClip Click;

    //ボリューム
    float VolumeScale = 2f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void RetryGameButton()
    {
        // Retryボタンが押されたときの処理
        Invoke("LoadScene", 2.1f);

        // HeavyBandit_0のアニメーション再生
        if (heavyBanditAnimator != null)
        {
            audioSource.PlayOneShot(Hukkatu2);
            audioSource.PlayOneShot(Hukkatu);
            heavyBanditAnimator.SetTrigger("IsHukkatu");
        }
    }

    public void TitleButton()
    {
        audioSource.PlayOneShot(Click, VolumeScale);
        SceneManager.LoadScene("Title");
    }

    void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Game_Death")
        {
            SceneManager.LoadScene("Game");
        }

        if (SceneManager.GetActiveScene().name == "Stage2_Death")
        {
            SceneManager.LoadScene("Stage2");
        }

        if (SceneManager.GetActiveScene().name == "Stage3_Death")
        {
            SceneManager.LoadScene("Stage3");
        }

        if (SceneManager.GetActiveScene().name == "Stage4_Death")
        {
            SceneManager.LoadScene("Stage4");
        }
    }
}

