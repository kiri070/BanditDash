using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject configPanel;

    //���ʉ�
    AudioSource audioSource;
    public AudioClip KlickSound;

    //�{�����[��
    float VolumeScale = 2f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //�S�[���h�����[�h
        GoldManager.HaveGold = PlayerPrefs.GetInt("HAVEGOLD", 0);
        GoldManager.Gold = PlayerPrefs.GetInt("HAVEGOLD", 0);

        //�|�[�V�����������[�h
        PotionManager.Potion = PlayerPrefs.GetInt("HAVEPOTION", 0);
    }

    public void ShowConfigPanel()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        configPanel.SetActive(true);
        
    }

    public void HideConfigPanel()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        configPanel.SetActive(false);

    }

    //�Q�[�����~
    public void StopButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        Time.timeScale = 0;�@//0�Œ�~
    }

    //�Q�[�����ĊJ
    public void SaikaiButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        Time.timeScale = 1;�@//1���f�t�H���g
    }


    public void Stage1StartButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Game");
    }

    public void Stage4StartButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Stage4");
    }

    public void Stage2StartButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3StartButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Stage3");
    }

    public void StageSelectButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("StageSelect");
    }

    public void TitleButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Title");
    }

    public void ScoreButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Score");
    }

    public void ShopButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        SceneManager.LoadScene("Shop");
    }

    public void RetryGameButton()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        Invoke("LoadScene", 0.2f);
    }

    public void RetryStage2Button()
    {
        audioSource.PlayOneShot(KlickSound, VolumeScale);
        Invoke("LoadScene", 0.2f);
    }

    public void EscButton()
    {
        Application.Quit();
    }

    //�V�[���ϑJ����
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
    }
}