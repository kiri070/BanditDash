using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PotionManager : MonoBehaviour
{
    //�e�L�X�g
    private Text potionText;

    //�|�[�V����
    public static int Potion = 0;
    public static int HavePotion = 0;

    //��
    AudioSource audio;
    public AudioClip BuySound;
    public AudioClip CannotButSound;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        potionText = GetComponent<Text>();

        //�S�[���h�����[�h
        GoldManager.HaveGold = PlayerPrefs.GetInt("HAVEGOLD", 0);
        GoldManager.Gold = PlayerPrefs.GetInt("HAVEGOLD", 0);

        //�|�[�V�����������[�h
        Potion = PlayerPrefs.GetInt("HAVEPOTION", 0);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            potionText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            potionText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            potionText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            potionText.text = "0";
        }

        else if (SceneManager.GetActiveScene().name == "Shop")
        {
            potionText.text = PlayerPrefs.GetInt("HAVEPOTION", 0).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            //Game�V�[���Ŗ����\�L
            potionText.text = Potion.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }


        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //Stage2�V�[���Ŗ����\�L
            potionText.text = Potion.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            //Stage2�V�[���Ŗ����\�L
            potionText.text = Potion.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            //Stage2�V�[���Ŗ����\�L
            potionText.text = Potion.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        if (SceneManager.GetActiveScene().name == "Shop")
        {
            //Stage2�V�[���Ŗ����\�L
            potionText.text = Potion.ToString();

            //Score�V�[���Ō��݂̖�����\�L
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        //�|�[�V��������ۑ�
        PlayerPrefs.SetInt("HAVEPOTION", Potion);

        PlayerPrefs.Save();
    }

    //�񕜃|�[�V�����𔃂��{�^��
    public void HealPotionBuyButton()
    {
        //�K�v�S�[���h���A�|�[�V�����̍ő吔
        if (GoldManager.Gold >= 700 && GoldManager.HaveGold >= 700 && Potion < 3)
        {
            audio.PlayOneShot(BuySound);

            GoldManager.Gold -= 700;
            GoldManager.HaveGold -= 700;
            Potion += 1;

            //�S�[���h��ۑ�
            PlayerPrefs.SetInt("HAVEGOLD", GoldManager.HaveGold);
            PlayerPrefs.SetInt("HAVEGOLD", GoldManager.Gold);

            //�|�[�V��������ۑ�
            PlayerPrefs.SetInt("HAVEPOTION", Potion);

            GoldManager.goldText.text = GoldManager.Gold.ToString();

            PlayerPrefs.Save();
        }

        else
        {
            //�����Ȃ��Ƃ��̉�
            audio.PlayOneShot(CannotButSound);
        }
       
    }

    //���Z�b�g�{�^��
    public void MoneyResetButton()
    {
        Potion = 0;
        PlayerPrefs.DeleteKey("HAVEPOTION");
        PlayerPrefs.SetInt("HAVEPOTION", Potion);
        PlayerPrefs.Save();
        potionText.text = Potion.ToString();

        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        // �A�v���P�[�V�������I������O�� PlayerPrefs ��ۑ�
        PlayerPrefs.Save();
    }
}
