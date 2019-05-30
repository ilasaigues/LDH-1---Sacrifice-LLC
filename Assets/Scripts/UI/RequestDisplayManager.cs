using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestDisplayManager : MonoBehaviour
{
    public RequestDisplay requestDisplayPrefab;
    List<RequestDisplay> displays = new List<RequestDisplay>();
    // Start is called before the first frame update
    void Start()
    {
        RequestSystem.Instance.OnNewRequest += NewRequest;
    }

    // Update is called once per frame
    void Update()
    {
        float prefabHeight = requestDisplayPrefab.GetComponent<RectTransform>().rect.height;

        displays.Sort(new System.Comparison<RequestDisplay>((d1, d2) => { return d1.request.remainingTime.CompareTo(d2.request.remainingTime); }));
        for (int i = 0; i < displays.Count; i++)
        {
            RequestDisplay display = displays[i];
            if (display == null)
            {
                displays.RemoveAt(i);
                i--;
                continue;
            }
            Vector3 position = display.transform.localPosition;
            position.y = -prefabHeight * i - prefabHeight / 2f - 5;
            display.transform.localPosition = Vector3.Lerp(display.transform.localPosition, position, .3333333f);
        }
    }

    public void NewRequest(RequestSystem.Request newRequest)
    {
        RequestDisplay newDisplay = Instantiate(requestDisplayPrefab, transform);
        newDisplay.Initialize(newRequest);
        displays.Add(newDisplay);
    }
}
