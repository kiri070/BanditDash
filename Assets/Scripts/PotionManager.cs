using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PotionManager : MonoBehaviour
{
    //テキスト
    private Text potionText;

    //ポーション
    public static int Potion = 0;
    public static int HavePotion = 0;

    //音
    AudioSource audio;
    public AudioClip BuySound;
    public AudioClip CannotButSound;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        potionText = GetComponent<Text>();

        //ゴールドをロード
        GoldManager.HaveGold = PlayerPrefs.GetInt("HAVEGOLD", 0);
        GoldManager.Gold = PlayerPrefs.GetInt("HAVEGOLD", 0);

        //ポーション数をロード
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
            //Gameシーンで枚数表記
            potionText.text = Potion.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }


        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //Stage2シーンで枚数表記
            potionText.text = Potion.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            //Stage2シーンで枚数表記
            potionText.text = Potion.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            //Stage2シーンで枚数表記
            potionText.text = Potion.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        if (SceneManager.GetActiveScene().name == "Shop")
        {
            //Stage2シーンで枚数表記
            potionText.text = Potion.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEPOTION", HavePotion);

        }

        //ポーション数を保存
        PlayerPrefs.SetInt("HAVEPOTION", Potion);

        PlayerPrefs.Save();
    }

    //回復ポーションを買うボタン
    public void HealPotionBuyButton()
    {
        //必要ゴールド数、ポーションの最大数
        if (GoldManager.Gold >= 700 && GoldManager.HaveGold >= 700 && Potion < 3)
        {
            audio.PlayOneShot(BuySound);

            GoldManager.Gold -= 700;
            GoldManager.HaveGold -= 700;
            Potion += 1;

            //ゴールドを保存
            PlayerPrefs.SetInt("HAVEGOLD", GoldManager.HaveGold);
            PlayerPrefs.SetInt("HAVEGOLD", GoldManager.Gold);

            //ポーション数を保存
            PlayerPrefs.SetInt("HAVEPOTION", Potion);

            GoldManager.goldText.text = GoldManager.Gold.ToString();

            PlayerPrefs.Save();
        }

        else
        {
            //買えないときの音
            audio.PlayOneShot(CannotButSound);
        }
       
    }

    //リセットボタン
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
        // アプリケーションが終了する前に PlayerPrefs を保存
        PlayerPrefs.Save();
    }
}
