using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoldManager : MonoBehaviour
{
    public static Text goldText; //PotionManagerからアクセスできるようstatic
    public static int Gold = 0; //ゲーム中に表記されるもの
    public static int HaveGold = 0; //Scoreシーンで表記されるもの

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
            //Gameシーンで枚数表記
            goldText.text = Gold.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }


        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            //Stage2シーンで枚数表記
            goldText.text = Gold.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            //Stage2シーンで枚数表記
            goldText.text = Gold.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            //Stage2シーンで枚数表記
            goldText.text = Gold.ToString();

            //Scoreシーンで現在の枚数を表記
            PlayerPrefs.SetInt("HAVEGOLD", HaveGold);

        }

        PlayerPrefs.Save();
    }


    //リセットボタン
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
        // アプリケーションが終了する前に PlayerPrefs を保存
        PlayerPrefs.Save();
    }
}
