using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RequestDisplay : MonoBehaviour
{
    public UIProgressBar progressBar;
    public List<Image> displays = new List<Image>();
    private RequestSystem.Request request;

    public void Initialize(RequestSystem.Request newRequest)
    {
        request = newRequest;
        for (int i = 0; i < displays.Count; i++)
        {
            if (newRequest.items.Count > i)
            {
                displays[i].sprite = newRequest.items[i].sprite;
            }
            else
            {
                displays[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (request != null)
        {
            progressBar.SetProgress(request.remainingTime / request.startTime);
            if (request.satisfied || request.remainingTime < 0) Destroy(gameObject);
        }
    }
}
