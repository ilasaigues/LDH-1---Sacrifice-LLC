using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    public GameObject fullView;
    public GameObject winView;
    public GameObject loseView;
    public GameObject[] stars;

    public Text resultText;
    public Text winText;

    static string TEXT_RESULT_DEFEAT = "THE GODS ARE FURIOUS!";
    static string TEXT_RESULT_VICTORY = "THE GODS ARE SATED!";
    static string TEXT_SCORE = "You scored {0} points.";
    static string TEXT_SCORE_MISSING = " You need {0} points for the next star.";

    private void Start()
    {
        LevelEntity.Instance.OnSuccess += OnSuccess;
        LevelEntity.Instance.OnDefeat += OnDefeat;
    }

    public void OnDefeat()
    {
        fullView.SetActive(true);
        loseView.SetActive(true);
        winView.SetActive(false);

        resultText.text = TEXT_RESULT_DEFEAT;
    }

    public void OnSuccess(int score)
    {
        fullView.SetActive(true);
        winView.SetActive(true);
        loseView.SetActive(false);
        resultText.text = TEXT_RESULT_VICTORY;
        string winTextTemp = string.Format(TEXT_SCORE, score);
        string missingScoreText = "";
        for (int i = 2; i >= 0; i--)
        {
            if (score >= LevelEntity.Instance.levelData.starThresholds[i])
            {
                stars[i].SetActive(true);
            }
            else
            {
                stars[i].SetActive(false);
                missingScoreText = string.Format(TEXT_SCORE_MISSING, LevelEntity.Instance.levelData.starThresholds[i]);
            }
        }
        winText.text = winTextTemp + missingScoreText;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
