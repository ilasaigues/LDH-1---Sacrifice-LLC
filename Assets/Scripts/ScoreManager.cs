using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{


    public AnimationCurve reputationCurve;

    public FloatReference reputationGainMult;
    public FloatReference reputationLossMult;

    public float AAAreputationGainMult = 2;
    public float AAAreputationLossMult = 3;
    public int score { get; private set; }


    public System.Action<int> OnScoreUpdate = (newScore) => { };
    public System.Action<float> OnReputationUpdate = (newRep) => { };
    public System.Action<int, int> OnRequestCountUpdate = (full, fail) => { };

    private float reputation = 50;


    private int fulfilled;
    private int failed;




    public void RequestEnd(RequestSystem.Request request, bool succeeded)
    {
        if (succeeded)
        {
            AddScore(GetScoreFromWeight(request.weight));
            float timeMult = (request.remainingTime / request.startTime) + 0.5f;

            reputation = Mathf.Clamp(reputation + request.weight * timeMult * AAAreputationGainMult, 0, 100);
            fulfilled++;
        }
        else
        {
            reputation -= request.weight * AAAreputationLossMult;
            failed++;
        }
        OnRequestCountUpdate(fulfilled, failed);
        OnReputationUpdate(reputation / 100f);

    }

    public void AddScore(float addedScore)
    {
        score += Mathf.FloorToInt(addedScore) * 10;
        OnScoreUpdate(score);
    }

    public float GetScoreFromWeight(int weight)
    {
        return Fibonacci(weight + 1) * 10 * reputationCurve.Evaluate(reputation / 100f);
    }


    int Fibonacci(int step)
    {
        if (step <= 0) return 0;
        if (step == 1) return 1;
        return (Fibonacci(step - 1) + Fibonacci(step - 2));
    }

}
