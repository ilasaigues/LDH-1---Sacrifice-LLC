using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void SetProgress(float progress)
    {
        transform.localScale = new Vector3(Mathf.Clamp01(progress), 1, 1);
    }
    public void Visible(bool enabled)
    {
        spriteRenderer.gameObject.SetActive(enabled);
    }
}
