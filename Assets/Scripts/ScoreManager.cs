using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;

    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {

        scoreText = GetComponent<Text>();

        //�ۑ������l������Ă���B��������HIGHSCORE�ɒl�������Ă��Ȃ��Ƃ��̐��l
        PlayerPrefs.GetInt("HIGHSCORE", 0);



        if (SceneManager.GetActiveScene().name == "Game")
        {
            scoreText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            scoreText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            scoreText.text = "0";
        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            scoreText.text = "0";
        }

        else if (SceneManager.GetActiveScene().name == "Score")
        {
            scoreText.text = PlayerPrefs.GetInt("HIGHSCORE", 0).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Game")
        {
            scoreText.text = score.ToString();

            if (score > PlayerPrefs.GetInt("HIGHSCORE", 0))
            {
                //�ȈՓI�ȕۑ��@�\
                PlayerPrefs.SetInt("HIGHSCORE", score);
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage2")
        {
            scoreText.text = score.ToString();

            if (score > PlayerPrefs.GetInt("HIGHSCORE", 0))
            {
                //�ȈՓI�ȕۑ��@�\
                PlayerPrefs.SetInt("HIGHSCORE", score);
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            scoreText.text = score.ToString();

            if (score > PlayerPrefs.GetInt("HIGHSCORE", 0))
            {
                //�ȈՓI�ȕۑ��@�\
                PlayerPrefs.SetInt("HIGHSCORE", score);
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            scoreText.text = score.ToString();

            if (score > PlayerPrefs.GetInt("HIGHSCORE", 0))
            {
                //�ȈՓI�ȕۑ��@�\
                PlayerPrefs.SetInt("HIGHSCORE", score);
            }
        }
    }

    //���Z�b�g�{�^��
    //public void ScoreResetButton()
    //{
    //    score = 0;
    //    PlayerPrefs.DeleteKey("HIGHSCORE");
    //    PlayerPrefs.SetInt("HIGHSCORE", score);
    //    scoreText.text = score.ToString();
    //}
}




