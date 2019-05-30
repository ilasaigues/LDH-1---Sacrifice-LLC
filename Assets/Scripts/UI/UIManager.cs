using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    public Text scoreText;
    public Text requestCountText;
    public UIProgressBar reputationBar;
    public Image reputationBarImage;
    public Gradient reputationGradient;

    const string TEXT_SCORE = "Score: {0}";
    const string TEXT_REQUEST_COUNT = "Fulfilled / Failed: {0} / {1}";

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.OnReputationUpdate += ReputationUpdated;
        ScoreManager.Instance.OnScoreUpdate += ScoreUpdated;
        ScoreManager.Instance.OnRequestCountUpdate += RequestCountUpdated;
        RequestCountUpdated(0, 0);
        ScoreUpdated(0);
        reputationBar.SetProgress(.5f);
        reputationBarImage.color = reputationGradient.Evaluate(.5f);
    }


    void ReputationUpdated(float newRep)
    {
        reputationBar.SetProgress(newRep);
        reputationBarImage.color = reputationGradient.Evaluate(newRep);
    }

    void ScoreUpdated(int newScore)
    {
        scoreText.text = scoreText.text = string.Format(TEXT_SCORE, newScore);
    }

    private void RequestCountUpdated(int fulfilled, int failed)
    {
        requestCountText.text = string.Format(TEXT_REQUEST_COUNT, fulfilled, failed);
    }

}
