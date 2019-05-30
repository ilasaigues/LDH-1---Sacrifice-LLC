using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIProgressBar : MonoBehaviour
{

    public Image bar;

    public void SetProgress(float progress)
    {
        bar.fillAmount = Mathf.Clamp01(progress);
    }

}
