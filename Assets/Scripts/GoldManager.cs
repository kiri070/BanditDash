using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoldManager : MonoBehaviour
{
    public static Text goldText; //PotionManager����A�N�Z�X�ł���悤static
    public static int Gold = 0; //�Q�[�����ɕ\�L��������
    public static int HaveGold = 0; //Score�V�[���ŕ\�L��������

    // Start is called before the first frame update
    void Start()
    {
        goldText = GetComponent<Text>();

        PlayerPrefs.GetInt("HAVEGOLD", 0);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            goldText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            goldText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            goldText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            goldText.text = "0";
        }

        else if (SceneManager.GetActiveScene().name == "Score" || SceneManager.GetActiveScene().name == "Shop")
        {
            goldText.text = PlayerPrefs.GetInt("HAVEGOLD", 0).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            //Game�V�[���Ŗ����\�L
            goldText.text = Gold.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }


        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //Stage2�V�[���Ŗ����\�L
            goldText.text = Gold.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            //Stage2�V�[���Ŗ����\�L
            goldText.text = Gold.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            //Stage2�V�[���Ŗ����\�L
            goldText.text = Gold.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }

        PlayerPrefs.Save();
    }


    //���Z�b�g�{�^��
    //public void MoneyResetButton()
    //{
    //    Gold = 0;
    //    PlayerPrefs.DeleteKey("HAVEGOLD");
    //    PlayerPrefs.SetInt("HAVEGOLD", Gold);
    //    PlayerPrefs.Save();
    //    goldText.text = Gold.ToString();

    //    PlayerPrefs.Save();
    //}

    void OnDestroy()
    {
        // �A�v���P�[�V�������I������O�� PlayerPrefs ��ۑ�
        PlayerPrefs.Save();
    }
}
